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
    private HealthSO _healthScriptableObject;

    private float _currentHealth;

    protected bool _isDead = false;

    private void Awake()
    {
       _currentHealth = this._healthScriptableObject.MaxHealthPoints; // Current health is at the max

        Debug.Log($"Current heath = {_currentHealth}.");
    }

    /// <summary>
    /// Call these functions with events. Set the behaviour on death in inherited scripts
    /// </summary>

    public void TakeDamage()
    {
        if(_currentHealth > 0)
            _currentHealth--; // decrement health if it is not at zero

        Debug.Log($"Current heath = {_currentHealth}.");

        if(_currentHealth <= 0)
        {
            _isDead = true; // Use this bool in the child class to run the Die() function
            Debug.Log($"_isDead == {_isDead}");

            _currentHealth = 0; // Stop decrementing

            TryHealth();
        }
    }

    public void Heal()
    {

        if(_currentHealth < _healthScriptableObject.MaxHealthPoints)
            _currentHealth++; // increment health if we aren't already at the max

        Debug.Log($"Current heath = {_currentHealth}.");

        if (_currentHealth >= _healthScriptableObject.MaxHealthPoints)
        {
            _currentHealth = _healthScriptableObject.MaxHealthPoints; // Stop incrementing health if we are already at the max
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
