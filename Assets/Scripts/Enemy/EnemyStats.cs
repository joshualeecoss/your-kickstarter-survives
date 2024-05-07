using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyStats : MonoBehaviour, IPooledEnemy
{
    public EnemyScriptableObject enemyData;

    // Current Stats:
    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentDamage;
    [HideInInspector]
    public float knockbackForce;
    [HideInInspector]
    public float experience;
    public float despawnDistance = 50f;
    private Transform player;
    private bool isDead = false;

    // Variables to implement damage feedback
    [Header("Damage Feedback")]
    public Color damageColour = new Color(1, 1, 1, 1);
    public float damageFlashDuration = 0.2f;
    public float deathFadeTime = 0.6f;
    Color originalColour;
    SpriteRenderer sr;
    EnemyMovement movement;

    float optimizerCooldown;
    public float optimizerCooldownDur;
    float opDist;

    Animator animator;

    public void OnEnemySpawn()
    {
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;
        knockbackForce = enemyData.Knockback;
        experience = enemyData.Experience;
        sr.color = originalColour;

        GetComponent<Collider2D>().enabled = true;
        isDead = false;
    }

    void Awake()
    {
        player = FindObjectOfType<PlayerStats>().transform;

        // Get a reference to the sprite sr, and enemy movement script. Save the original sprite colour
        sr = GetComponent<SpriteRenderer>();
        originalColour = sr.color;
        movement = GetComponent<EnemyMovement>();

        if (GetComponent<Animator>())
        {
            animator = GetComponent<Animator>();
            animator.SetBool("walking", true);
        }

    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, player.position) >= despawnDistance)
        {
            ReturnEnemyOptimizer();
        }
    }

    public void TakeDamage(float dmg, Vector2 sourcePosition, float knockbackDuration = 0.2f)
    {
        currentHealth -= dmg;
        StartCoroutine(DamageFlash());

        // Create text popup when enemy takes damage
        if (dmg > 0)
        {
            GameManager.GenerateFloatingText(Mathf.FloorToInt(dmg).ToString(), transform);
        }

        // Apply knockback if it is not zero
        if (knockbackForce > 0)
        {
            // Get direction of knockback
            Vector2 dir = (Vector2)transform.position - sourcePosition;
            movement.Knockback(dir.normalized * knockbackForce, knockbackDuration);
        }

        // Kills the enemy if health drops below zero
        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    // Coroutine that makes the enemy flash when taking damage
    IEnumerator DamageFlash()
    {
        if (animator)
        {
            animator.SetBool("walking", false);
        }
        sr.color = damageColour;
        yield return new WaitForSeconds(damageFlashDuration);
        sr.color = originalColour;
        if (animator)
        {
            animator.SetBool("walking", true);
        }
    }

    public void Kill()
    {
        isDead = true;
        if (animator)
        {
            animator.SetBool("walking", false);
        }
        transform.GetComponent<Collider2D>().enabled = false;
        StartCoroutine(KillFade());
    }

    // Coroutine that fades enemy away slowly when killed
    IEnumerator KillFade()
    {
        // Waits for a single frame
        WaitForEndOfFrame w = new WaitForEndOfFrame();
        float t = 0, origAlpha = sr.color.a;

        GetComponent<DropRateManager>().GetDrop();

        // This loop fires every frame
        while (t < deathFadeTime)
        {
            yield return w;
            t += Time.deltaTime;

            // Set the colour for this frame
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, (1 - t / deathFadeTime) * origAlpha);
        }
        EnemyDespawn();
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && !isDead)
        {
            PlayerStats player = col.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(currentDamage);
        }
    }

    void ReturnEnemy()
    {
        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
        transform.position = player.position + enemySpawner.relativeSpawnPoints[Random.Range(0, enemySpawner.relativeSpawnPoints.Count)].position;
    }

    public void EnemyDespawn()
    {
        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
        enemySpawner.OnEnemyKilled();
        gameObject.SetActive(false);
    }

    void ReturnEnemyOptimizer()
    {
        optimizerCooldown -= Time.deltaTime;

        if (optimizerCooldown <= 0f)
        {
            optimizerCooldown = optimizerCooldownDur;
        }
        else
        {
            return;
        }

        opDist = Vector3.Distance(player.transform.position, transform.position);
        if (opDist > despawnDistance)
        {
            ReturnEnemy();
        }

    }
}
