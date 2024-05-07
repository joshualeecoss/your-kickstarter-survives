using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    CharacterScriptableObject characterData;

    float currentHealth;
    float maxHealth;
    float currentRecovery;
    float currentMoveSpeed;
    float currentMight;
    float currentProjectileSpeed;
    float currentMagnet;
    float currentArea;
    int currentAmount;
    int currentPierce;
    float currentCooldown;
    float currentArmor;

    #region Current Stats Properties


    public float CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            // Check if the value has changed
            if (currentHealth != value)
            {
                currentHealth = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentHealthDisplay.text = "Health: " + currentHealth;
                }
                //Update the real time value of the stat
                //Add any additional logic here that needs to be executed when the value changes
            }
        }
    }

    public float MaxHealth
    {
        get { return maxHealth; }
        set
        {
            if (maxHealth != value)
            {
                maxHealth = value;
            }
        }
    }

    public float CurrentRecovery
    {
        get { return currentRecovery; }
        set
        {
            if (currentRecovery != value)
            {
                currentRecovery = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentRecoveryDisplay.text = "Recovery: " + currentRecovery;
                }
            }
        }
    }

    public float CurrentMoveSpeed
    {
        get { return currentMoveSpeed; }
        set
        {
            if (currentMoveSpeed != value)
            {
                currentMoveSpeed = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentMoveSpeedDisplay.text = "Move Speed: " + currentMoveSpeed;
                }
            }
        }
    }

    public float CurrentMight
    {
        get { return currentMight; }
        set
        {
            if (currentMight != value)
            {
                currentMight = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentMightDisplay.text = "Might: " + currentMight;
                }
            }
        }
    }

    public float CurrentProjectileSpeed
    {
        get { return currentProjectileSpeed; }
        set
        {
            if (currentProjectileSpeed != value)
            {
                currentProjectileSpeed = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentProjectileSpeedDisplay.text = "Projectile Speed: " + currentProjectileSpeed;
                }
            }
        }
    }

    public float CurrentMagnet
    {
        get { return currentMagnet; }
        set
        {
            if (currentMagnet != value)
            {
                currentMagnet = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentMagnetDisplay.text = "Magnet: " + currentMagnet;
                }
            }
        }
    }

    public float CurrentArea
    {
        get { return currentArea; }
        set
        {
            if (currentArea != value)
            {
                currentArea = value;
            }
        }
    }

    public int CurrentAmount
    {
        get { return currentAmount; }
        set
        {
            if (currentAmount != value)
            {
                currentAmount = value;
            }
        }
    }

    public int CurrentPierce
    {
        get { return currentPierce; }
        set
        {
            if (currentPierce != value)
            {
                currentPierce = value;
            }
        }
    }

    public float CurrentCooldown
    {
        get { return currentCooldown; }
        set
        {
            if (currentCooldown != value)
            {
                currentCooldown = value;
            }
        }
    }

    public float CurrentArmor
    {
        get { return currentArmor; }
        set
        {
            if (currentArmor != value)
            {
                currentArmor = value;
            }
        }
    }
    #endregion

    public ParticleSystem damageEffect;

    [Header("Experience/Level")]
    public float experience = 0;
    public int level = 1;
    public float experienceCap;
    public float expBarCap;
    public float expToFill;

    [Header("I-Frames")]
    public float invincibilityDuration;
    private float invincibilityTimer;
    private bool isInvincible;

    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }

    public List<LevelRange> levelRanges;

    private InventoryManager inventory;
    public int weaponIndex;
    public int passiveItemIndex;

    [Header("UI")]
    public Image healthBar;
    public Image expBar;
    public TextMeshProUGUI levelText;

    Animator animator;
    SpriteRenderer characterSprite;

    void Awake()
    {
        characterData = CharacterSelector.GetData();
        inventory = GetComponent<InventoryManager>();

        MaxHealth = characterData.MaxHealth;
        CurrentHealth = characterData.MaxHealth;
        CurrentRecovery = characterData.Recovery;
        CurrentMoveSpeed = characterData.MoveSpeed;
        CurrentMight = characterData.Might;
        CurrentProjectileSpeed = characterData.ProjectileSpeed;
        CurrentMagnet = characterData.Magnet;
        CurrentArea = characterData.Area;
        CurrentAmount = characterData.Amount;
        CurrentPierce = characterData.Pierce;
        CurrentCooldown = characterData.Cooldown;
        CurrentArmor = characterData.Armor;

        // Set the animator
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = characterData.Animator;

        // Set the character sprite and sprite size
        characterSprite = GetComponent<SpriteRenderer>();
        characterSprite.sprite = characterData.CharacterSprite;
        characterSprite.size = characterSprite.size * 2;

        SpawnWeapon(characterData.StartingWeapon);
    }

    void Start()
    {
        experienceCap = levelRanges[0].experienceCapIncrease;
        CharacterSelector.instance.DestroySingleton();

        //Set the current stats display
        GameManager.instance.currentHealthDisplay.text = "Health: " + currentHealth;
        GameManager.instance.currentRecoveryDisplay.text = "Recovery: " + currentRecovery;
        GameManager.instance.currentMoveSpeedDisplay.text = "Move Speed: " + currentMoveSpeed;
        GameManager.instance.currentMightDisplay.text = "Might: " + currentMight;
        GameManager.instance.currentProjectileSpeedDisplay.text = "Projectile Speed: " + currentProjectileSpeed;
        GameManager.instance.currentMagnetDisplay.text = "Magnet: " + currentMagnet;

        GameManager.instance.AssignChosenCharacterUI(characterData);
        UpdateHealthBar();
        UpdateExpBar();
        UpdateLevelText();

        expBarCap = levelRanges[0].experienceCapIncrease;
    }

    void Update()
    {
        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
        else if (isInvincible)
        {
            isInvincible = false;
        }
        Recover();
    }

    public void IncreaseExperience(float amount)
    {
        experience += amount;
        LevelUpChecker();
        UpdateExpBar();
    }

    private void LevelUpChecker()
    {
        if (experience >= experienceCap)
        {
            experience -= experienceCap;
            level++;
            float experienceCapIncrease = 0;
            foreach (LevelRange range in levelRanges)
            {
                if (level >= range.startLevel && level <= range.endLevel)
                {
                    experienceCapIncrease = range.experienceCapIncrease;
                    break;
                }
            }
            experienceCap += experienceCapIncrease;
            UpdateLevelText();
            GameManager.instance.StartLevelUp();
        }
    }

    void UpdateExpBar()
    {
        // Update exp bar fill amount
        expBar.fillAmount = experience / experienceCap;
    }

    void UpdateLevelText()
    {
        // Update level text in exp bar
        levelText.text = "LV " + level.ToString();
    }

    public void TakeDamage(float dmg)
    {
        if (!isInvincible)
        {
            if (dmg - currentArmor > 0)
            {
                CurrentHealth -= (dmg - currentArmor);
                if (damageEffect)
                {
                    Destroy(Instantiate(damageEffect, transform.position, Quaternion.identity), 5f);
                }

                SetInvincibility(invincibilityDuration);
                isInvincible = true;
            }

            if (CurrentHealth <= 0)
            {
                Kill();
            }

            UpdateHealthBar();
        }
    }

    void UpdateHealthBar()
    {
        healthBar.fillAmount = CurrentHealth / MaxHealth;
    }

    private void Kill()
    {
        if (!GameManager.instance.isGameOver)
        {
            GameManager.instance.AssignLevelReachedUI(level);
            GameManager.instance.AssignChosenWeaponsAndPassiceItemsUI(inventory.weaponUISlots, inventory.passiveItemUISlots);
            GameManager.instance.GameOver();
        }
    }

    public void RestoreHealth(float amount)
    {
        if (CurrentHealth < MaxHealth)
        {
            CurrentHealth += amount;

            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
        }
        UpdateHealthBar();
    }

    private void Recover()
    {
        if (CurrentHealth < MaxHealth)
        {
            CurrentHealth += currentRecovery * Time.deltaTime;

            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
        }
        UpdateHealthBar();
    }

    public void SpawnWeapon(GameObject weapon)
    {
        // checking if the slots are full
        if (weaponIndex >= inventory.weaponSlots.Count)
        {
            Debug.LogError("Inventory slots already full");
            return;
        }

        // Spawn weapon
        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform);
        inventory.AddWeapon(weaponIndex, spawnedWeapon.GetComponent<WeaponController>()); // Add weapon to it's inventory slot
        weaponIndex++; //increase index so slots don't overlap
    }

    public void SpawnPassiveItem(GameObject passiveItem)
    {
        // Check if slots are full
        if (passiveItemIndex >= inventory.passiveItemSlots.Count)
        {
            Debug.LogError("Inventory slots already full");
            return;
        }

        // Spawn passive item
        GameObject spawnedPassiveItem = Instantiate(passiveItem, transform.position, Quaternion.identity);
        spawnedPassiveItem.transform.SetParent(transform);
        inventory.AddPassiveItem(passiveItemIndex, spawnedPassiveItem.GetComponent<PassiveItem>());
        passiveItemIndex++;
    }

    public void SetInvincibility(float duration)
    {
        invincibilityTimer = duration;
        isInvincible = true;
    }

}
