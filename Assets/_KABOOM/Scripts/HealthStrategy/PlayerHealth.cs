using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerHealth : BaseHealth, IHealth
{
    string _currentScene;

    [SerializeField]
    private HealSO _healthPackScriptableObject;

    private void Start()
    {
        this.CurrentHealthType = this;

        _currentScene = SceneManager.GetActiveScene().name;

        LevelManager.Instance.EventData.OnHealthChangedEvent?.Invoke(CurrentHealth);
    }
    public void Health()
    {
        Die();
    }

    public void Die()
    {
        if(_isDead == true)
            SceneManager.LoadScene(_currentScene); // Reloads the current scene when you die. Will need to be replaced with better logic later
    }

    protected override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);

        LevelManager.Instance.EventData.OnHealthChangedEvent?.Invoke(CurrentHealth);
    }

    protected override void Heal(float amount)
    {
        base.Heal(amount);

        LevelManager.Instance.EventData.OnHealthChangedEvent?.Invoke(CurrentHealth);
    }

    // Had to separate ontriggerenter and oncollisionenter. Keeping the switch cuz we might need it later

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Projectile":

                Debug.Log("This is a turret projectile.");

                TakeDamage(_damageScriptableObject.TurretDamage); // Take the amount of damage the turrets deal
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "InstantDeathBox":

                Debug.Log("This is an instant death box.");
                _isDead = true; // For instakills, like when you fall in acid
                Die();
                break;

            case "ExplosionTrigger":

                Debug.Log("This is an explosion.");
                Die(); // Changed from having a certain amount of damage.
                break;

            case "HealthPack":

                Debug.Log("This is a health pack.");
                Heal(_healthPackScriptableObject.HealhPackAmount); // Heal the amount a health pack heals
                break;

        }
    }
}
