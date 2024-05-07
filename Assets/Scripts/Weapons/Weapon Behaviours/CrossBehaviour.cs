using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossBehaviour : ProjectileWeaponBehaviour
{
    public Vector2 closestEnemyDirection;
    Rigidbody2D rb;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();

        rb.AddForce(closestEnemyDirection * currentSpeed, ForceMode2D.Impulse);
    }

    void FixedUpdate()
    {
        rb.velocity += -currentSpeed * 1.75f * Time.fixedDeltaTime * closestEnemyDirection;
        float rigidbodyDrag = Mathf.Clamp01(1.0f - (rb.drag * Time.fixedDeltaTime));
        rb.velocity *= rigidbodyDrag;
        transform.position += (Vector3)(rb.velocity * Time.fixedDeltaTime);
        rb.AddTorque(180 * Time.deltaTime);
    }

    protected override void ReducePierce()
    {
        // This weapon ignores pierce
        return;
    }
}
