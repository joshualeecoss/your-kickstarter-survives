using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MightPassiveItem : PassiveItem
{
    protected override void ApplyModifier()
    {
        player.CurrentMight *= passiveItemData.Multiplier;
    }
}
