using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : WeaponController
{
    [SerializeField]
    float spread = 0.5f;

    protected override void Attack()
    {
        base.Attack();
        StartCoroutine(FireProjectiles());
    }

    protected override IEnumerator FireProjectiles()
    {
        for (int i = 0; i < currentAmount; i++)
        {
            GameObject spawnedBullet = Instantiate(weaponData.Prefab);

            Vector3 position = new Vector3(transform.position.x, transform.position.y);
            if (currentAmount > 1)
            {
                if (playerController.facingVector == PlayerController.FacingVector.horizontal)
                {
                    position.y += Random.Range(-spread, spread); // spreading the bullets along a line
                }
                if (playerController.facingVector == PlayerController.FacingVector.vertical)
                {
                    position.x += Random.Range(-spread, spread);
                }

            }

            spawnedBullet.transform.position = position;
            // spawnedBullet.transform.parent = transform;
            spawnedBullet.GetComponent<GunBehaviour>().DirectionChecker(playerController.lastMovedVector);
            yield return new WaitForSeconds(currentInterval);
        }
        attackComplete = true;
    }

}
