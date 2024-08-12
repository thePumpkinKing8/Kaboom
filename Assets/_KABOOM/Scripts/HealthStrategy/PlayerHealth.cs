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
        if(IsDead == true)
            SceneManager.LoadScene(_currentScene); // Reloads the current scene when you die. Will need to be replaced with better logic later
    }
}
