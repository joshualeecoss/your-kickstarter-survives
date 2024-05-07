using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceGem : Pickup
{
    PlayerStats playerStats;
    public float experience;


    public override void Collect()
    {
        if (hasBeenCollected)
        {
            return;
        }
        else
        {
            base.Collect();
        }
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            FindObjectOfType<PlayerStats>().IncreaseExperience(experience);
            col.GetComponentInChildren<PlayerCollector>().collectiblesInRange.Remove(transform);
            FindObjectOfType<PickupOptimizer>().spawnedPickups.Remove(gameObject);
            Destroy(gameObject);
        }
    }

}
