using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterScriptableObject", menuName = "ScriptableObjects/Character")]
public class CharacterScriptableObject : ScriptableObject
{
    [SerializeField]
    Sprite icon;
    public Sprite Icon { get => icon; private set => icon = value; }

    [SerializeField]
    Sprite characterSprite;
    public Sprite CharacterSprite { get => characterSprite; private set => characterSprite = value; }

    [SerializeField]
    RuntimeAnimatorController animator;
    public RuntimeAnimatorController Animator { get => animator; private set => animator = value; }

    [SerializeField]
    string characterName;
    public string CharacterName { get => characterName; private set => characterName = value; }

    [SerializeField]
    [TextArea]
    string description;
    public string Description { get => description; private set => description = value; }

    [SerializeField]
    GameObject startingWeapon;
    public GameObject StartingWeapon { get => startingWeapon; private set => startingWeapon = value; }

    [SerializeField]
    float maxHealth;
    public float MaxHealth { get => maxHealth; private set => maxHealth = value; }

    [SerializeField]
    float recovery;
    public float Recovery { get => recovery; private set => recovery = value; }

    [SerializeField]
    float moveSpeed;
    public float MoveSpeed { get => moveSpeed; private set => moveSpeed = value; }

    [SerializeField]
    float might;
    public float Might { get => might; private set => might = value; }

    [SerializeField]
    float projectileSpeed;
    public float ProjectileSpeed { get => projectileSpeed; private set => projectileSpeed = value; }

    [SerializeField]
    float magnet;
    public float Magnet { get => magnet; private set => magnet = value; }

    [SerializeField]
    float area;
    public float Area { get => area; private set => area = value; }

    [SerializeField]
    int amount;
    public int Amount { get => amount; private set => amount = value; }

    [SerializeField]
    int pierce;
    public int Pierce { get => pierce; private set => pierce = value; }

    [SerializeField]
    float cooldown;
    public float Cooldown { get => cooldown; private set => cooldown = value; }

    [SerializeField]
    float armor;
    public float Armor { get => armor; private set => armor = value; }
}
