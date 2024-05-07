using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoin : Pickup
{
    public int goldValue;

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
            GameManager.instance.AddGoldToTotal(goldValue);
            col.GetComponentInChildren<PlayerCollector>().collectiblesInRange.Remove(transform);
            FindObjectOfType<PickupOptimizer>().spawnedPickups.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
