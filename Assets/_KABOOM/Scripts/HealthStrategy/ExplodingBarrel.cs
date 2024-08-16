using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class ExplodingBarrel : BaseHealth, IHealth
{
    private GameObject _explosionTrigger;

    private void Start()
    {
        this.CurrentHealthType = this;
    }

    public void Health()
    {
        Die();
    }

    public void Die()
    {
        _explosionTrigger.SetActive(true); // Sets the trigger as active

        // Some logic for explosions/event to trigger explosion logic here

        float explosionDelay = 0.1f; // Makes sure the object doesn't get destroyed before executing all its logic

        Destroy(this.gameObject, explosionDelay);
    }

    private void Awake()
    {
        _explosionTrigger = FindChildWithTag("ExplosionTrigger"); // Finds the trigger object
        _explosionTrigger.SetActive(false); // Sets the trigger as inactive
    }

    GameObject FindChildWithTag(string tag)
    {
        // Looks for a specific child object with a tag
        foreach (Transform child in transform)
        {
            if (child.CompareTag(tag))
                return child.gameObject;
        }

        return null;
    }

    // Needs the player laser in one of these
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Projectile":

                TakeDamage(_damageScriptableObject.TurretDamage); // Take the amount of damage the turrets deal
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "ExplosionTrigger":

                Die();
                break;
        }
    }
}
