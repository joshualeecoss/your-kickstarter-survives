using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipController : WeaponController
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        attackComplete = false;
        base.Attack();
        StartCoroutine(FireWhip());
    }

    IEnumerator FireWhip()
    {
        float interval;
        int alternator = 1;
        for (int i = 0; i < currentAmount; i++)
        {
            GameObject spawnedWhip = Instantiate(weaponData.Prefab);
            if (playerController.facing == PlayerController.Facing.right)
            {
                spawnedWhip.transform.position = new Vector2(transform.position.x + alternator * (spawnedWhip.transform.localScale.x * currentArea) / 2, transform.position.y); // add half the width of the whip to the x position to spawn in the middle of the player
                spawnedWhip.transform.localScale *= alternator;
            }
            else if (playerController.facing == PlayerController.Facing.left)
            {
                spawnedWhip.transform.position = new Vector2(transform.position.x - alternator * (spawnedWhip.transform.localScale.x * currentArea) / 2, transform.position.y); // subtract half the width of the whip from the x position to spawn in the middle of the player
                spawnedWhip.transform.localScale *= -alternator;
            }
            spawnedWhip.transform.parent = transform;
            if (currentAmount > 1)
            {
                interval = currentInterval;
            }
            else
            {
                interval = 0f;
            }
            alternator *= -1;
            yield return new WaitForSeconds(interval);
        }
        attackComplete = true;
    }
}
