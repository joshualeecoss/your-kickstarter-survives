using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireballBehaviour : ProjectileWeaponBehaviour
{
    public Vector2 aimDirection;

    void Update()
    {
        transform.position += currentSpeed * Time.deltaTime * (Vector3)aimDirection;
    }
}
