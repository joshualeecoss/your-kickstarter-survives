using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownPassiveItem : PassiveItem
{
    protected override void ApplyModifier()
    {
        player.CurrentCooldown -= passiveItemData.Multiplier;
    }
}
