using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPooler : Pooler
{
    #region Singleton
    public static PickupPooler Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exists");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        IPooledPickup pooledObj = objectToSpawn.GetComponent<IPooledPickup>();

        if (pooledObj != null)
        {
            pooledObj.OnPickupSpawn();
        }

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
