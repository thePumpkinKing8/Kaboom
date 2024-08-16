using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _healthText; // Player health

    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private GameObject[] _keySprites; // Display for how many keys are left. Add as many as there are in the level

    private PlayerHealth _healthScript;

    private int _keyCount; // How many keys have been picked up

    private void Awake()
    {
        _healthScript = _player.GetComponent<PlayerHealth>();

        LevelManager.Instance.EventData.OnHealthChangedEvent.AddListener(UpdateHealthUI);
        LevelManager.Instance.EventData.KeyCollectedEvent.AddListener(UpdateKeyUI);

        // Initialize the UI with the current health
        UpdateHealthUI(_healthScript.CurrentHealth);

        foreach(GameObject keySprite in _keySprites)
        {
            keySprite.SetActive(false); // Hides all the key sprites at the start
        }
    }

    private void OnDisable()
    {
        if(_healthScript != null)
        {
            LevelManager.Instance.EventData.OnHealthChangedEvent.RemoveListener(UpdateHealthUI);
        }
    }

    public void UpdateHealthUI(float amount)
    {
        _healthText.text = amount.ToString(); // Update health UI
    }

    public void UpdateKeyUI(string str = "default")
    {
        if(_keyCount < _keySprites.Length) // If there are more keys to pick up
        {
            _keySprites[_keyCount].SetActive(true); // Set the next active key that needs to be activated
            _keyCount++; // Increment the amount of keys
        }
    }
}
