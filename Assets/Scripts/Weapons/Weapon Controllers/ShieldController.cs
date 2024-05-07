using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : WeaponController
{
    GameObject spawnedShield;
    int currentCharges;
    Color[] chargeColours = {
        Color.blue,
        Color.green,
        Color.yellow
    };
    SpriteRenderer sr;

    protected override void Start()
    {
        base.Start();
        spawnedShield = Instantiate(weaponData.Prefab);
        spawnedShield.transform.position = transform.position;
        spawnedShield.transform.parent = transform;
        currentCharges = 0;
        spawnedShield.SetActive(false);
    }

    protected override void Update()
    {
        if (spawnedShield.GetComponent<ShieldBehaviour>().currentCharges < currentAmount)
        {
            currentCooldown -= Time.deltaTime;
            if (currentCooldown <= 0f)
            {
                ChargeShield();
                SetCurrentCooldown();
                if (spawnedShield.GetComponent<ShieldBehaviour>().currentCharges > 0)
                {
                    spawnedShield.SetActive(true);
                }
            }
        }

        if (spawnedShield.GetComponent<ShieldBehaviour>().currentCharges == 0)
        {
            spawnedShield.SetActive(false);
        }

        else
        {
            currentCharges = spawnedShield.GetComponent<ShieldBehaviour>().currentCharges;
        }

        sr = spawnedShield.GetComponent<SpriteRenderer>();
        Color spriteColour = sr.color;
        switch (currentCharges)
        {
            case 1:
                spriteColour = chargeColours[0];
                break;
            case 2:
                spriteColour = chargeColours[1];
                break;
            case 3:
                spriteColour = chargeColours[2];
                break;
            default:
                spriteColour = chargeColours[0];
                break;
        }
        spriteColour.a = 0.25f;

        sr.color = spriteColour;
    }

    protected void ChargeShield()
    {
        spawnedShield.GetComponent<ShieldBehaviour>().currentCharges++;
        currentCharges = spawnedShield.GetComponent<ShieldBehaviour>().currentCharges;
    }

    protected override void SetCurrentAmount()
    {
        currentAmount = weaponData.Amount;
    }
}
