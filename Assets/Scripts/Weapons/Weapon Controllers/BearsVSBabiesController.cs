using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearsVSBabiesController : WeaponController
{
    public List<GameObject> markedEnemies; //For hitbox delay so enemies can't be hit consecutively with projectiles
    public float hitboxDelay = 1.7f;
    float currentHitboxDelay;

    public Sprite[] sprites;

    protected override void Start()
    {
        base.Start();
        markedEnemies = new List<GameObject>();
        currentHitboxDelay = hitboxDelay;
    }

    protected override void Update()
    {
        base.Update();
        // The same enemy should not be hit multiple times by the weapon, so if the enemy is
        // marked the hitbox delay begins counting down. The delay applies to all enemies.
        if (markedEnemies.Count >= 1)
        {
            currentHitboxDelay -= Time.deltaTime;
            if (currentHitboxDelay <= 0)
            {
                markedEnemies.Clear();
            }
        }
        else
        {
            currentHitboxDelay = hitboxDelay;
        }
    }

    protected override void Attack()
    {
        base.Attack();
        float radius = currentArea;
        for (int i = 0; i < currentAmount; i++)
        {
            GameObject spawnedBible = Instantiate(weaponData.Prefab);
            float angle = 360.0f * i / currentAmount;
            spawnedBible.GetComponent<SpriteRenderer>().sprite = sprites[i % sprites.Length];
            spawnedBible.transform.position = transform.position + Quaternion.Euler(0, 0, angle) * Vector2.right * radius;
            spawnedBible.transform.parent = transform;
            spawnedBible.transform.localScale *= currentArea;
        }
    }


}
