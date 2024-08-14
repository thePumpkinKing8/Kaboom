using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class ExplodingBarrel : BaseHealth, IHealth
{
    private GameObject _explosionTrigger;

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

    private void Awake()
    {
        _explosionTrigger = FindChildWithTag("ExplosionTrigger"); // Finds the trigger object
        _explosionTrigger.SetActive(false); // Sets the trigger as inactive
    }

    private void Start()
    {
        this.CurrentHealthType = this;
    }

    public void Health()
    {
        Explode();
    }

    public void Explode()
    {
        _explosionTrigger.SetActive(true); // Sets the trigger as active

        // Some logic for explosions/event to trigger explosion logic here

        float explosionDelay = 0.1f; // Makes sure the object doesn't get destroyed before executing all its logic

        Destroy(this.gameObject, explosionDelay);
    }

    // Testing
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.K))
    //    {
    //        this.TakeDamage(_damageScriptableObject.TurretDamage);
    //    }
    //}

}
