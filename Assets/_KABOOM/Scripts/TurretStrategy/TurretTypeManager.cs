using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TurretShooting))]
public class TurretTypeManager : MonoBehaviour
{
    private TurretShooting _turretShooting;

    private void Awake()
    {
        _turretShooting = GetComponent<TurretShooting>();

        if(_turretShooting == null)
        {
            Debug.LogError("TurretShooting component is missing!");
        }
    }
    private void Start()
    {
        
    }
}
