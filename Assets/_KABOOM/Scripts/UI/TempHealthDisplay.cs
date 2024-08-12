using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TempHealthDisplay : MonoBehaviour
{
    [SerializeField]
    private BaseHealth _healthObject;

    [SerializeField]
    private TextMeshProUGUI _theText;

    private float _healthNumber;

    private void Awake()
    {
        _healthObject.GetComponent<BaseHealth>();

        _healthNumber = _healthObject.CurrentHealth;
    }

    private void Update()
    {
        _healthNumber = _healthObject.CurrentHealth;
        _theText.text = "Health = " + _healthNumber.ToString();
    }
}
