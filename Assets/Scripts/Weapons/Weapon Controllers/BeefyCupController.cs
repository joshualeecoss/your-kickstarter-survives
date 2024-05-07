using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeefyCupController : WeaponController
{
    protected override void Attack()
    {
        base.Attack();
        StartCoroutine(FireProjectiles());
    }

    protected override IEnumerator FireProjectiles()
    {
        float launchAngle = 90;

        for (int i = 0; i < currentAmount; i++)
        {
            GameObject spawnedCup = Instantiate(weaponData.Prefab);
            float directionFloat = 0;
            if (playerController.lastFacing == PlayerController.Facing.right)
            {
                directionFloat = 1;
            }
            else if (playerController.lastFacing == PlayerController.Facing.left)
            {
                directionFloat = -1;
            }
            spawnedCup.GetComponent<BeefyCupBehaviour>().SetLaunchAngle(launchAngle);
            spawnedCup.transform.position = transform.position;
            spawnedCup.transform.parent = transform;

            launchAngle -= 5f * directionFloat;

            yield return new WaitForSeconds(currentInterval);
        }
        attackComplete = true;
    }


}
