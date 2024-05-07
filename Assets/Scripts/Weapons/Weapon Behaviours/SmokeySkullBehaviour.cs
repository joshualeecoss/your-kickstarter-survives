using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SmokeySkullBehaviour : ProjectileWeaponBehaviour
{
    public Vector2 closestEnemyDirection;

    void Update()
    {
        transform.position += currentSpeed * Time.deltaTime * (Vector3)closestEnemyDirection;
    }

    public override void DirectionChecker(Vector3 dir)
    {
        direction = dir;

        float dirx = direction.x;
        float diry = direction.y;

        float angle = Mathf.Atan2(diry, dirx) * Mathf.Rad2Deg + 180;

        transform.eulerAngles = Vector3.forward * angle;

        if (angle > 90f && angle < 270f)
        {
            transform.GetComponent<SpriteRenderer>().flipY = true;
        }
    }
}
