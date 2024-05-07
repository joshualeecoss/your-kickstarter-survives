using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearsVSBabiesBehaviour : ProjectileWeaponBehaviour
{
    BearsVSBabiesController controller;
    Vector3 rotateAxis;

    protected override void Start()
    {
        Destroy(gameObject, currentDuration);
        controller = GetComponentInParent<BearsVSBabiesController>();
        rotateAxis = new Vector3(0, 0, 1);
        //transform.localScale *= currentArea;
    }

    void Update()
    {
        RotateAroundPoint(transform.parent.position, rotateAxis, currentSpeed * 5 * Time.deltaTime);
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy") && !controller.markedEnemies.Contains(col.gameObject))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage, transform.position);
            controller.markedEnemies.Add(col.gameObject);
        }
        else if (col.CompareTag("Prop"))
        {
            if (col.gameObject.TryGetComponent(out BreakableProps breakable) && !controller.markedEnemies.Contains(col.gameObject))
            {
                breakable.TakeDamage(currentDamage);
                controller.markedEnemies.Add(col.gameObject);
            }
        }
    }

    void RotateAroundPoint(Vector3 center, Vector3 axis, float angle)
    {
        // transform.RotateAround(transform.parent.position, new Vector3(0, 0, 1), currentSpeed * 5 * Time.deltaTime);

        Quaternion rot = Quaternion.AngleAxis(angle, axis);
        Vector3 pos = transform.position;
        Vector3 dir = pos - center;
        dir = rot * dir;
        transform.position = center + dir;
    }

    void OnDestroy()
    {
        controller.attackComplete = true;
    }
}
