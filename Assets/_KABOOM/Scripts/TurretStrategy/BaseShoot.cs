using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    protected float _shotSpeed = 1f; // How fast the bullet comes out
    [SerializeField]
    protected float _shotDelay = 2f; // Time between shots
    [SerializeField]
    protected int _amountOfProjectiles = 1; // How many bullets per shot
    [SerializeField]
    protected bool _isHoming = false; // Whether the bullets track the player or not
   
    protected int _bulletPoolAmount = 10; // How many bullets pooled. KEEP HIGH ENOUGH for the speed and amount of projectiles to avoid out of range exceptions

    protected float _bulletLifetime = 1f; // How long until bullets are set inactive

    protected float _shotStagger = 0.1f; // Stagger shots in case of shooting multiple bullets at a time

    private PoolManager _poolManager;
    //protected List<GameObject> _pooledBullets = new List<GameObject>(); // List of the projectiles for the pool

    private Rigidbody2D _bulletRB;

    private void Awake()
    {
        SharedInstance = this;


        _bulletRB = _bulletPrefab.GetComponent<Rigidbody2D>();

        _poolManager = PoolManager.Instance;
    }

 

    private void Update()
    {
        // Only shoot if a shot is not currently being fired
        if (_currentlyShooting == false)
        {
            TryShoot();

            StartCoroutine(WaitToShoot());
        }
    }

    private IEnumerator WaitToShoot()
    {
        _currentlyShooting = true; // In the process of firing a shot

        yield return new WaitForSeconds(_shotDelay);

        PoolObject obj = _poolManager.Spawn("TurretProjectile");
        TurretProjectile bullet = obj.GetComponent<TurretProjectile>();

        for(int i = 0; i <_amountOfProjectiles; i++)
        {
            if (bullet != null)
            {
                // Put the bullets at the spawn point (opening of the turret barrel)
                bullet.transform.position = _shotSpawnPoint.position;
                bullet.transform.rotation = _shotSpawnPoint.rotation;
                //we need a generic projectile script so that typing dosent become an issue
                bullet.Speed = _shotSpeed;
                bullet.Direction = _shotSpawnPoint.right;// Shoot the bullet forward
            }

            yield return new WaitForSeconds(_shotStagger); // In the event of multiple bullets per volley

        }

        _currentlyShooting = false; // Allows this coroutine to be called again

        //yield return new WaitForSeconds(_bulletLifetime);

        //DespawnProjectile(bullet);
       // bullet.SetActive(false); // Deactivate bullet after its lifetime
    }

    private void TryShoot()
    {
        CurrentShotType?.Shoot(); // Sets the turret type
    }

}
