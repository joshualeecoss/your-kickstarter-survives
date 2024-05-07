using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base script for all weapon controllers
/// </summary>
public class WeaponController : MonoBehaviour
{
    [Header("Weapon Stats")]
    public WeaponScriptableObject weaponData;
    public float currentCooldown;
    public int currentAmount;
    public float currentInterval;
    public float currentArea;
    public float currentSpeed;
    public bool attackComplete = false;

    protected PlayerController playerController;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        SetCurrentCooldown();
        SetCurrentAmount();
        currentInterval = weaponData.Interval;
        SetCurrentArea();
        SetCurrentSpeed();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (attackComplete)
        {
            currentCooldown -= Time.deltaTime;
            if (currentCooldown <= 0f)
            {
                Attack();
            }
        }
    }

    protected virtual void Attack()
    {
        attackComplete = false;
        SetCurrentArea();
        SetCurrentAmount();
        SetCurrentCooldown();
        //SetCurrentSpeed();
    }

    protected virtual IEnumerator FireProjectiles()
    {
        yield return new WaitForSeconds(currentInterval);
        attackComplete = true;
    }

    protected virtual void SetCurrentArea()
    {
        currentArea = weaponData.Area * playerController.GetComponent<PlayerStats>().CurrentArea;
    }

    protected virtual void SetCurrentAmount()
    {
        currentAmount = weaponData.Amount + playerController.GetComponent<PlayerStats>().CurrentAmount;
    }

    protected virtual void SetCurrentSpeed()
    {
        currentSpeed = weaponData.Speed * playerController.GetComponent<PlayerStats>().CurrentProjectileSpeed;
    }

    protected virtual void SetCurrentCooldown()
    {
        currentCooldown = weaponData.Cooldown * playerController.GetComponent<PlayerStats>().CurrentCooldown;
    }


}
