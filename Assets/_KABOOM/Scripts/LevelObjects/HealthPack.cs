using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            float pickupDelay = 0.1f; // Makes sure the object doesn't get destroyed before executing all its logic
            Destroy(this.gameObject, pickupDelay); // Destroy the health pack when it is picked up
        }
    }
}
