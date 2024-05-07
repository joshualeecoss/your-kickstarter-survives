using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponBehaviour : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    public float destroyAfterSeconds;

    // Current Stats:
    protected float currentDamage;
    protected float currentSpeed;
    protected float currentArea;
    protected float currentInterval;
    protected float currentDuration;

    protected virtual void Awake()
    {
        SetCurrentDamage();
        SetCurrentSpeed();
        SetCurrentArea();
        SetCurrentInterval();
        SetCurrentDuration();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
        transform.localScale *= currentArea;
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage, transform.position);
        }
        else if (col.CompareTag("Prop"))
        {
            if (col.gameObject.TryGetComponent(out BreakableProps breakable))
            {
                breakable.TakeDamage(currentDamage);
            }
        }
    }

    public void SetCurrentDamage()
    {
        currentDamage = (float)Mathf.CeilToInt(weaponData.Damage * FindObjectOfType<PlayerStats>().CurrentMight);
    }

    public void SetCurrentArea()
    {
        currentArea = weaponData.Area * FindObjectOfType<PlayerStats>().CurrentArea;
    }

    public void SetCurrentSpeed()
    {
        currentSpeed = weaponData.Speed * FindObjectOfType<PlayerStats>().CurrentProjectileSpeed;
    }

    public void SetCurrentInterval()
    {
        currentInterval = weaponData.Interval;
    }

    public void SetCurrentDuration()
    {
        currentDuration = weaponData.Duration;
    }

}
