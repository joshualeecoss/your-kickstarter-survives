using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeefyCupBehaviour : ProjectileWeaponBehaviour
{
    Rigidbody2D rb;
    float launchAngle;
    float[] rotationSpeeds = { -360, -270, 270, 360 };

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();

        AddForceAtAnAngle(currentSpeed, launchAngle);
    }

    void FixedUpdate()
    {
        rb.velocity += (Vector2)(Physics.gravity * Time.fixedDeltaTime);
        float rigidbodyDrag = Mathf.Clamp01(1.0f - (rb.drag * Time.fixedDeltaTime));
        rb.velocity *= rigidbodyDrag;
        transform.position += (Vector3)(rb.velocity * Time.fixedDeltaTime);
        rb.AddTorque(rotationSpeeds[Random.Range(0, rotationSpeeds.Length)] * Time.deltaTime);
    }

    public override void DirectionChecker(Vector3 dir)
    {
        direction = dir;

        float dirx = direction.x;

        Vector3 scale = transform.localScale;
        Vector3 position = transform.position;

        if (dirx < 0)
        {
            scale.x *= -1;
        }

        transform.localScale = scale;
        transform.position = position;
    }

    public void SetLaunchAngle(float angle)
    {
        launchAngle = angle;
    }

    public void AddForceAtAnAngle(float force, float angle)
    {
        angle *= Mathf.Deg2Rad;
        float xComponent = Mathf.Cos(angle) * force;
        float yComponent = Mathf.Sin(angle) * force;
        Vector2 forceApplied = new Vector3(xComponent, yComponent);

        rb.AddForce(forceApplied, ForceMode2D.Impulse);
    }
}
