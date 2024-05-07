using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeySkullController : WeaponController
{
    protected override void Attack()
    {
        base.Attack();
        StartCoroutine(FireProjectiles());
    }

    protected override IEnumerator FireProjectiles()
    {
        for (int i = 0; i < currentAmount; i++)
        {
            GameObject spawnedProjectile = Instantiate(weaponData.Prefab);
            spawnedProjectile.transform.position = transform.position;
            Vector2 enemyDirection = DirectionToClosestEnemy();
            spawnedProjectile.GetComponent<SmokeySkullBehaviour>().closestEnemyDirection = enemyDirection;
            spawnedProjectile.GetComponent<SmokeySkullBehaviour>().DirectionChecker(enemyDirection);
            spawnedProjectile.transform.parent = transform;

            yield return new WaitForSeconds(currentInterval);
        }
        attackComplete = true;
    }


    Vector2 DirectionToClosestEnemy()
    {
        float distanceToClosestEnemy = Mathf.Infinity;
        GameObject closestEnemy = null;
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (allEnemies.Length != 0)
        {
            foreach (GameObject currentEnemy in allEnemies)
            {
                float distanceToEnemy = (currentEnemy.transform.position - transform.position).sqrMagnitude;
                if (distanceToEnemy < distanceToClosestEnemy)
                {
                    distanceToClosestEnemy = distanceToEnemy;
                    closestEnemy = currentEnemy;
                }
            }
            return (closestEnemy.transform.position - transform.position).normalized;
        }
        return Vector2.right;
    }
}
