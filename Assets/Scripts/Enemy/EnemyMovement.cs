using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    EnemyStats enemy;
    Transform player;

    Vector2 knockbackVelocity;
    float knockbackDuration;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<EnemyStats>();
        player = FindObjectOfType<PlayerController>().transform;
    }

    void FixedUpdate()
    {
        // If currently being knocked back, then process the knockback
        if (knockbackDuration > 0)
        {
            transform.position += (Vector3)knockbackVelocity * Time.deltaTime;
            knockbackDuration -= Time.deltaTime;
        }
        else
        {
            if (enemy.currentHealth > 0)
            {
                //otherwise constantly move the enemy towards the player as long as the enemy is still alive
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemy.currentMoveSpeed * Time.deltaTime);
            }
            elsed
            {
                enemy.GetComponent<Collider2D>().enabled = false;
            }
        }
    }

    public void Knockback(Vector2 velocity, float duration)
    {
        if (knockbackDuration > 0)
        {
            return;
        }
        knockbackVelocity = velocity;
        knockbackDuration = duration;
    }
}
