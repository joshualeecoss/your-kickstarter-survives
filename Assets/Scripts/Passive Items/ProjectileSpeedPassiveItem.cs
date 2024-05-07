using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpeedPassiveItem : PassiveItem
{
    protected override void ApplyModifier()
    {
        player.CurrentProjectileSpeed *= passiveItemData.Multiplier;
    }
}
