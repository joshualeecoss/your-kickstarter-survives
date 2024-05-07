using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponEvolutionBlueprint", menuName = "ScriptableObjects/WeaponEvolutionBluprint")]
public class WeaponEvolutionBlueprint : ScriptableObject
{
    public WeaponScriptableObject baseWeaponData;
    public PassiveItemScriptableObject catalystPassiveItemData;
    public WeaponScriptableObject evolvedWeapnData;
    public GameObject evolvedWeapon;
}
