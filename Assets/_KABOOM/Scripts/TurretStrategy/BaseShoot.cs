using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// Base class for all the different types of turret shooting. Repeated code should be added here, as well as variables to use with the child classes. Extended variables must be declared as protected.
/// </summary>
public class BaseShoot : MonoBehaviour
{
    public IShoot CurrentShotType; // The specific type of shooting to be used

    public static BaseShoot SharedInstance; // For the bullet pool

    private bool _currentlyShooting = false;

    [SerializeField]
    protected Transform _shotSpawnPoint; // Where the bullets come from

    [SerializeField]
    protected GameObject _bulletPrefab; // The bullet

    [SerializeField]
    protected float _shotSpeed; // How fast the bullet comes out

    [SerializeField]
    protected float _shotDelay; // Time between shots

    [SerializeField]
    protected float _amountOfProjectiles; // How many bullets per shot

    protected bool _isHoming = false; // Whether the bullets track the player or not

    protected int _bulletPoolAmount = 20; // How many bullets pooled

    protected List<GameObject> _pooledBullets; // List of the projectiles for the pool

    protected Vector2 _shotDiretion;

    private void Awake()
    {
        SharedInstance = this;
    }

    private void Start()
    {
        _pooledBullets = new List<GameObject>();
        GameObject bullet = _bulletPrefab;

        // For as many bullets as we've declared for the pool:
        for(int i = 0; i < _bulletPoolAmount; i++)
        {
            bullet = Instantiate(_bulletPrefab); // Instantiate bullet

            bullet.SetActive(false); // Hides the bullet until it's used

            _pooledBullets.Add(bullet); // Add this bullet to the pool
        }
    }

    private void Update()
    {
        // Only shoot if a shot is not currently being fired
        if(_currentlyShooting == false)
        {
            StartCoroutine(WaitToShoot());
        }
    }

    protected void ShootBullet()
    {
        _bulletPrefab.transform.Translate(transform.right * _shotSpeed * Time.deltaTime); // Move the bullet forward at the correct speed
    }

    private IEnumerator WaitToShoot()
    {
        _currentlyShooting = true;

        yield return new WaitForSeconds(_shotDelay); // Wait between shots
        ShootBullet(); // Shoot after waiting

        _currentlyShooting = false; // Allows the coroutine to be called again
    }









}
