using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRecoveryPassiveItem : PassiveItem
{
    protected override void ApplyModifier()
    {
        player.CurrentRecovery += passiveItemData.Multiplier;
    }
}
