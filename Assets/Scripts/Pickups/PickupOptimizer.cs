using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickupOptimizer : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> spawnedPickups;
    public float maxOpDist;
    float opDist;
    float optimizerCooldown;
    public float optimizerCooldownDur;
    public int maxPickups;

    void FixedUpdate()
    {
        Optimizer();
    }

    void Optimizer()
    {
        optimizerCooldown -= Time.deltaTime;

        if (optimizerCooldown <= 0f)
        {
            optimizerCooldown = optimizerCooldownDur;
        }
        else
        {
            return;
        }

        foreach (GameObject pickup in spawnedPickups.ToList())
        {
            if (pickup)
            {
                opDist = Vector3.Distance(player.transform.position, pickup.transform.position);
                if (opDist > maxOpDist)
                {
                    pickup.SetActive(false);
                }
                else
                {
                    pickup.SetActive(true);
                }
            }
        }

    }
}
