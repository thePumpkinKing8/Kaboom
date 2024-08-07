using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShoot : MonoBehaviour
{
    public IShoot CurrentShotType; // The specific type of shooting to be used

    public static BaseShoot SharedInstance; // For the bullet pool

    private bool _currentlyShooting = false;

    [SerializeField]
    protected Transform _shotSpawnPoint; // Where the bullets come from

    [SerializeField]
    protected GameObject _bulletPrefab; // The bullet

    protected float _shotSpeed = 1f; // How fast the bullet comes out

    protected float _shotDelay = 1f; // Time between shots

    protected int _amountOfProjectiles = 1; // How many bullets per shot

    protected bool _isHoming = false; // Whether the bullets track the player or not

    protected int _bulletPoolAmount = 10; // How many bullets pooled. KEEP HIGH ENOUGH for the speed and amount of projectiles to avoid out of range exceptions

    protected float _bulletLifetime = 1f; // How long until bullets are set inactive

    protected float _shotStagger = 0.1f; // Stagger shots in case of shooting multiple bullets at a time

    protected List<GameObject> _pooledBullets = new List<GameObject>(); // List of the projectiles for the pool

    private void Start()
    {
        
    }

    private void Update()
    {
        if(_currentlyShooting == false)
        {
            TryShoot();
        }
    }

    private void ShootProjectile()
    {

    }

    private void TryShoot()
    {
        CurrentShotType?.Shoot(); // Sets the turret type
    }
}
