using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour, ICollectible, IPooledPickup
{
    public bool hasBeenCollected = false;
    public bool moving = false;
    public string pickupTag;
    public float despawnDistance;
    Transform player;
    public float distance;

    public void OnPickupSpawn()
    {
        hasBeenCollected = false;
        moving = false;
        FindObjectOfType<PickupOptimizer>().spawnedPickups.Add(gameObject);
    }

    void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;
    }

    public virtual void Collect()
    {
        hasBeenCollected = true;
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponentInChildren<PlayerCollector>().collectiblesInRange.Remove(transform);
            FindObjectOfType<PickupOptimizer>().spawnedPickups.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    protected virtual void DespawnPickup()
    {

    }

}
