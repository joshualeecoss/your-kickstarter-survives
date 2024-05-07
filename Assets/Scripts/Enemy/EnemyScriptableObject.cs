using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObjects/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    // Enemy base stats
    [SerializeField]
    float moveSpeed;
    public float MoveSpeed { get => moveSpeed; private set => moveSpeed = value; }

    [SerializeField]
    float maxHealth;
    public float MaxHealth { get => maxHealth; private set => maxHealth = value; }

    [SerializeField]
    float damage;
    public float Damage { get => damage; private set => damage = value; }

    [SerializeField]
    float knockback;
    public float Knockback { get => knockback; private set => knockback = value; }

    [SerializeField]
    float experience;
    public float Experience { get => experience; private set => experience = value; }

    [SerializeField]
    string tag;
    public string Tag { get => tag; private set => tag = value; }
}
