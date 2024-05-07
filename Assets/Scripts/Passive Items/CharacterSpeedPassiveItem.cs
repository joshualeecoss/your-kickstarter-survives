using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpeedPassiveItem : PassiveItem
{
    protected override void ApplyModifier()
    {
        player.CurrentMoveSpeed *= passiveItemData.Multiplier;
    }
}
