using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehaviour : ProjectileWeaponBehaviour
{

    void Update()
    {
        transform.position += currentSpeed * Time.deltaTime * direction;
    }
}
