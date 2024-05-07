using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehaviour : MeleeWeaponBehaviour
{
    public int currentCharges;
    protected override void Awake()
    {
        currentInterval = weaponData.Interval;
        currentCharges = 0;
    }

    void Update()
    {
        if (currentInterval != weaponData.Interval)
        {
            currentInterval = weaponData.Interval;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            PlayerStats player = FindObjectOfType<PlayerStats>();
            player.SetInvincibility(currentInterval);
            currentCharges--;
        }
    }

}
