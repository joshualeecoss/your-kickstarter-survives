using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base script of all projectile behaviours
/// </summary>
public class ProjectileWeaponBehaviour : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    protected Vector3 direction;
    public float destroyAfterSeconds;


    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldown;
    protected int currentPierce;
    protected float currentDuration;
    protected float currentArea;


    void Awake()
    {
        SetCurrentDamage();
        SetCurrentSpeed();
        SetCurrentCooldown();
        SetCurrentPierce();
        SetCurrentDuration();
        SetCurrentArea();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
        transform.localScale *= currentArea;
    }

    public virtual void DirectionChecker(Vector3 dir)
    {
        direction = dir;

        float dirx = direction.x;
        float diry = direction.y;

        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;
        Vector3 position = transform.position;

        if (dirx < 0 && diry == 0) //left
        {
            scale.x *= -1;
            scale.y *= -1;
        }
        else if (dirx == 0 && diry < 0) //down
        {
            rotation.z = -90f;
        }
        else if (dirx == 0 && diry > 0) //up
        {
            rotation.z = 90f;
        }
        else if (dirx > 0 && diry > 0) //right up
        {
            rotation.z = 45f;
        }
        else if (dirx > 0 && diry < 0) //right down
        {
            rotation.z = -45f;
        }
        else if (dirx < 0 && diry > 0) //left up
        {
            // scale.x *= -1;
            // scale.y *= -1;
            rotation.z = 135f;
        }
        else if (dirx < 0 && diry < 0) //left down
        {
            // scale.x *= -1;
            // scale.y *= -1;
            rotation.z = -135f;
        }

        transform.localScale = scale;
        transform.rotation = Quaternion.Euler(rotation);
        transform.position = position;
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage, transform.position);
            ReducePierce();
        }
        else if (col.CompareTag("Prop"))
        {
            if (col.gameObject.TryGetComponent(out BreakableProps breakable))
            {
                breakable.TakeDamage(currentDamage);
                ReducePierce();
            }
        }
    }

    protected virtual void ReducePierce()
    {
        if (currentPierce == 0)
        {
            return;
        }
        else
        {
            currentPierce--;
            if (currentPierce <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetCurrentDamage()
    {
        currentDamage = weaponData.Damage * FindObjectOfType<PlayerStats>().CurrentMight;
    }

    public void SetCurrentPierce()
    {
        currentPierce = weaponData.Pierce + FindObjectOfType<PlayerStats>().CurrentPierce;
    }

    public void SetCurrentSpeed()
    {
        currentSpeed = weaponData.Speed * FindObjectOfType<PlayerStats>().CurrentProjectileSpeed;
    }

    public void SetCurrentArea()
    {
        currentArea = weaponData.Area * FindObjectOfType<PlayerStats>().CurrentArea;
    }

    public void SetCurrentCooldown()
    {
        currentCooldown = weaponData.Cooldown * FindObjectOfType<PlayerStats>().CurrentCooldown;
    }

    public void SetCurrentDuration()
    {
        currentDuration = weaponData.Duration;
    }

}
