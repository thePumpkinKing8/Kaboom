using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    /// <summary>
    /// We're using an interface again since some objects will have different effects on the game when they die vs others (for example, the player dying would reset the scene, while a turret dying would not
    /// </summary>

    public IHealth CurrentHealthType; // What type of object with health we're using

    [SerializeField]
    protected HealthSO _healthScriptableObject;

    [SerializeField]
    protected DamageSO _damageScriptableObject; // To store variables for how much damage each thing does

    protected bool _canTakeDamage;

    public float CurrentHealth { get; private set; }

    protected bool _isDead = false;

    private void Awake()
    {
       CurrentHealth = this._healthScriptableObject.MaxHealthPoints; // Current health is at the max

        Debug.Log($"Current heath = {CurrentHealth}.");
    }

    /// <summary>
    /// Call these functions with events. Set the behaviour on death in inherited scripts
    /// </summary>

    public void TakeDamage(float amount)
    {
        // The amount should be the damage modifier for whatever thing is causing something to take damage. Assigned in the child class

        if(CurrentHealth > 0)
            CurrentHealth -= amount; // decrement health if it is not at zero

        Debug.Log($"Current heath = {CurrentHealth}.");

        if(CurrentHealth <= 0)
        {
            _isDead = true; // Use this bool in the child class to run the Die() function
            Debug.Log($"_isDead == {_isDead}");

            CurrentHealth = 0; // Stop decrementing

            TryHealth();
        }
    }

    public void Heal()
    {

        if(CurrentHealth < _healthScriptableObject.MaxHealthPoints)
            CurrentHealth++; // increment health if we aren't already at the max

        Debug.Log($"Current heath = {CurrentHealth}.");

        if (CurrentHealth >= _healthScriptableObject.MaxHealthPoints)
        {
            CurrentHealth = _healthScriptableObject.MaxHealthPoints; // Stop incrementing health if we are already at the max
        }
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Q))
    //    {
    //        Heal();
    //    }

    //    if (Input.GetKeyDown(KeyCode.R))
    //    {
    //        TakeDamage();
    //    }
    //}

    private void TryHealth()
    {
        CurrentHealthType?.Health(); // Attempts to call the function from the child class
    }

}
