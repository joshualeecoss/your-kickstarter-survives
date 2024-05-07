using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaPassiveItem : PassiveItem
{
    protected override void ApplyModifier()
    {
        player.CurrentArea += passiveItemData.Multiplier;
    }
}
