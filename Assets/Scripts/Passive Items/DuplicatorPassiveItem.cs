using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuplicatorPassiveItem : PassiveItem
{
    protected override void ApplyModifier()
    {
        player.CurrentAmount += (int)passiveItemData.Multiplier;
    }
}
