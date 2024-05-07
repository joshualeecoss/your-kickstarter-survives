using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : WeaponController
{
    [SerializeField] float spread;

    protected override void Attack()
    {
        base.Attack();
        Vector2 direction = DirectionToRandomEnemy();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float startRotation = angle + spread / 2f;
        float angleIncrease;
        if (currentAmount <= 1)
        {   // making sure we're not dividing by 0 if currentAmount is only 1;
            angleIncrease = spread / currentAmount;
        }
        else
        {
            angleIncrease = spread / (currentAmount - 1f);
        }

        for (int i = 0; i < currentAmount; i++)
        {
            float tempRotation = startRotation - angleIncrease * i;
            GameObject spawnedFireball = Instantiate(weaponData.Prefab);
            spawnedFireball.transform.position = transform.position;
            spawnedFireball.transform.rotation = Quaternion.Euler(0f, 0f, tempRotation);
            spawnedFireball.GetComponent<FireballBehaviour>().aimDirection = new Vector2(Mathf.Cos(tempRotation * Mathf.Deg2Rad), Mathf.Sin(tempRotation * Mathf.Deg2Rad));
        }

        attackComplete = true;
    }

    // Find random enemy and get direction relative to player
    Vector2 DirectionToRandomEnemy()
    {
        GameObject randomEnemy = null;
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (allEnemies.Length != 0)
        {
            randomEnemy = allEnemies[Random.Range(0, allEnemies.Length)];

        }
        else { return Vector2.right; }
        return (randomEnemy.transform.position - transform.position).normalized;


    }
}
