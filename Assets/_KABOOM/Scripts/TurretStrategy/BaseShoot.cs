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

    protected float _shotSpeed; // How fast the bullet comes out

    protected float _shotDelay; // Time between shots

    protected float _amountOfProjectiles; // How many bullets per shot

    protected bool _isHoming = false; // Whether the bullets track the player or not

    protected int _bulletPoolAmount = 20; // How many bullets pooled

    protected float _bulletLifetime = 10f; // How long until bullets are set inactive

    protected List<GameObject> _pooledBullets; // List of the projectiles for the pool

    public GameObject GetPooledObject()
    {
        for(int i = 0; i < _bulletPoolAmount; i++)
        {
            if (!_pooledBullets[i].activeInHierarchy)
            {
                return _pooledBullets[i];
            }
        }

        return null;
    }

    private void Awake()
    {
        SharedInstance = this;
        Debug.Log("this script is being called wow");

        CreateBulletPool();

    }

    private void Start()
    {
        TryShoot();
    }

    private void CreateBulletPool()
    {
        _pooledBullets = new List<GameObject>();

        //For as many bullets as we've declared for the pool:
        for (int i = 0; i < _bulletPoolAmount; i++)
        {
            GameObject bullet = Instantiate(_bulletPrefab); // Instantiate bullet

            bullet.SetActive(false); // Hides the bullet until it's used

            _pooledBullets.Add(bullet); // Add this bullet to the pool
        }

        Debug.Log("successfully created pool");
    }

    private void Update()
    {
        // Only shoot if a shot is not currently being fired
        if (_currentlyShooting == false)
        {
            StartCoroutine(WaitToShoot());
        }
    }

    private IEnumerator WaitToShoot()
    {
        _currentlyShooting = true;

        yield return new WaitForSeconds(_shotDelay); // Wait between shots

        GameObject bullet = GetPooledObject();

        if (bullet != null)
        {
            bullet.transform.position = _shotSpawnPoint.position;
            bullet.transform.rotation = _shotSpawnPoint.rotation;
            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody2D>().velocity = _shotSpawnPoint.forward * _shotSpeed;
        }

        _currentlyShooting = false; // Allows the coroutine to be called again
    }

    private void TryShoot()
    {
        CurrentShotType?.Shoot();
        Debug.Log("tried to shoot");
    }

}
