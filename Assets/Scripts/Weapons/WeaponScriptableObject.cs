using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponScriptableObject", menuName = "ScriptableObjects/Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    [SerializeField]
    GameObject weaponPrefab;
    public GameObject Prefab { get => weaponPrefab; private set => weaponPrefab = value; }

    // Weapon base stats:
    [SerializeField]
    float damage;
    public float Damage { get => damage; private set => damage = value; }

    [SerializeField]
    float speed;
    public float Speed { get => speed; private set => speed = value; }

    [SerializeField]
    float cooldown;
    public float Cooldown { get => cooldown; private set => cooldown = value; }

    [SerializeField]
    float duration;
    public float Duration { get => duration; private set => duration = value; }

    [SerializeField]
    float area;
    public float Area { get => area; private set => area = value; }

    [SerializeField]
    int amount;
    public int Amount { get => amount; private set => amount = value; }

    [SerializeField]
    float interval;
    public float Interval { get => interval; private set => interval = value; }

    [SerializeField]
    int pierce;
    public int Pierce { get => pierce; private set => pierce = value; }

    [SerializeField]
    int level; //Not meant to be modified in game (only in editor)
    public int Level { get => level; private set => level = value; }

    [SerializeField]
    GameObject nextLevelControllerPrefab;
    public GameObject NextLevelPrefab { get => nextLevelControllerPrefab; private set => nextLevelControllerPrefab = value; }

    [SerializeField]
    new string name;
    public string Name { get => name; private set => name = value; }

    [SerializeField]
    string description;
    public string Description { get => description; private set => description = value; }

    [SerializeField]
    Sprite icon;
    public Sprite Icon { get => icon; private set => icon = value; }

    [SerializeField]
    int evolvedUpgradeToRemove;
    public int EvolvedUpgradeToRemove { get => evolvedUpgradeToRemove; private set => evolvedUpgradeToRemove = value; }

    [SerializeField]
    string identifier;
    public string Identifier { get => identifier; private set => identifier = value; }
}
