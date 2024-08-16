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
    private GameObject[] _keySprite; // Display for how many keys are left

    private PlayerHealth _healthScript;

    private void Awake()
    {
        _healthScript = _player.GetComponent<PlayerHealth>();

        _healthScript.OnHealthChanged.AddListener(UpdateHealthUI);

        // Initialize the UI with the current health
        UpdateHealthUI(_healthScript.CurrentHealth);
    }

    private void OnDisable()
    {
        if(_healthScript != null)
        {
            _healthScript.OnHealthChanged.RemoveListener(UpdateHealthUI);
        }
    }

    public void UpdateHealthUI(float amount)
    {
        _healthText.text = amount.ToString(); // Update health UI
    }
}
