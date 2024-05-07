using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    PlayerStats player;
    CircleCollider2D playerCollector;
    Transform collectibleTransform;
    public float pullSpeed;
    public List<Transform> collectiblesInRange = new List<Transform>();
    private float startingRadius;

    private void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        playerCollector = GetComponent<CircleCollider2D>();
        startingRadius = playerCollector.radius;
    }

    private void Update()
    {
        playerCollector.radius = startingRadius * player.CurrentMagnet;
        // copying collectiblesInRange to a seperate list in order to avoid modifying list while it's in use
        foreach (Transform item in collectiblesInRange.ToList())
        {
            if (item)
            {
                item.GetComponent<Pickup>().moving = true;
                item.position = Vector2.MoveTowards(item.position, transform.position, pullSpeed * Time.deltaTime);
            }
            else
            {
                collectiblesInRange.Remove(item);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out ICollectible collectible))
        {
            collectibleTransform = col.transform;
            collectiblesInRange.Add(collectibleTransform);

            collectible.Collect();
        }
    }
}
