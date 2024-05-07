using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoojerBehaviour : MeleeWeaponBehaviour
{
    List<GameObject> markedEnemies;
    Vector2 baseArea;

    protected override void Start()
    {
        markedEnemies = new List<GameObject>();
        baseArea = transform.localScale;
        transform.localScale = baseArea * currentArea;
    }

    void Update()
    {
        transform.localScale = baseArea * currentArea;
        if (currentDamage != weaponData.Damage * FindObjectOfType<PlayerStats>().CurrentMight)
        {
            currentDamage = weaponData.Damage * FindObjectOfType<PlayerStats>().CurrentMight;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy") && !markedEnemies.Contains(col.gameObject))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage, transform.position);
            markedEnemies.Add(col.gameObject);
        }
        else if (col.CompareTag("Prop"))
        {
            if (col.gameObject.TryGetComponent(out BreakableProps breakable) && !markedEnemies.Contains(col.gameObject))
            {
                breakable.TakeDamage(currentDamage);
                markedEnemies.Add(col.gameObject);
            }
        }
    }

    public void ClearMarkedEnemies()
    {
        markedEnemies.Clear();
    }

    public float GetCurrentArea()
    {
        return currentArea;
    }

    public void SetCurrentArea(float area)
    {
        currentArea = area;
    }
}
