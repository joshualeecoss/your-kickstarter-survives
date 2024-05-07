using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRateManager : MonoBehaviour
{
    [System.Serializable]
    public class Drops
    {
        public string name;
        public GameObject itemPrefab;
        public float dropRate;
    }
    GameObject pickupHolder;
    PickupOptimizer optimizer;
    EnemyStats enemyStats;
    PickupPooler pickupPooler;

    public List<Drops> drops;

    void Start()
    {
        pickupHolder = GameObject.Find("Pickup Optimizer");
        optimizer = FindObjectOfType<PickupOptimizer>();
        enemyStats = GetComponent<EnemyStats>();
        // pickupPooler = FindObjectOfType<PickupPooler>();
    }

    public void GetDrop()
    {
        if (!gameObject.scene.isLoaded)
        {
            return;
        }

        float randomNumber = Random.Range(0f, 100f);
        List<Drops> possibleDrops = new List<Drops>();

        foreach (Drops rate in drops)
        {
            if (randomNumber <= rate.dropRate)
            {
                possibleDrops.Add(rate);
            }
        }

        if (possibleDrops.Count > 0)
        {
            Drops drops = possibleDrops[Random.Range(0, possibleDrops.Count)];
            GameObject drop = Instantiate(drops.itemPrefab);
            if (drop.CompareTag("Gem"))
            {
                drop.GetComponent<ExperienceGem>().experience = enemyStats.experience;
                drop.transform.rotation = Quaternion.Euler(0, 0, 45);
            }
            drop.transform.position = transform.position;
            if (pickupHolder)
            {
                drop.transform.parent = pickupHolder.transform;
                optimizer.spawnedPickups.Add(drop);
            }
        }

    }
}
