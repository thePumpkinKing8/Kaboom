using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : BaseHealth, IHealth
{
    string _currentScene;

    private void Start()
    {
        this.CurrentHealthType = this;

        _currentScene = SceneManager.GetActiveScene().name;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("InstantDeathBox"))
        {
            _isDead = true; // For instakills, like when you fall in acid
            Die();
        }
    }
}
