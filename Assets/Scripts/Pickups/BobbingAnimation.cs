using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobbingAnimation : MonoBehaviour
{
    public float frequency; // speed of movement
    public float magnitude; // range of movement
    public Vector3 direction; // direction of movement
    private Vector3 initialPosition;
    Pickup pickup;

    void Start()
    {
        pickup = GetComponent<Pickup>();
        // Save the starting position of the game object
        initialPosition = pickup.transform.position;
    }

    void FixedUpdate()
    {
        if (pickup && !pickup.hasBeenCollected)
        {
            // Sine function for smooth bobbing effect
            pickup.transform.position = initialPosition + direction * Mathf.Sin(Time.time * frequency) * magnitude;
        }
    }

    public void SetInitialPosition(Vector3 position)
    {
        initialPosition = position;
    }
}
