using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private float _projectileVelocity;
    [Header("VisionCone Settings")]
    [SerializeField] private LayerMask _targetLayers;
    [SerializeField] private float _visionConeRange;
    [SerializeField] private float _visionConeHeight;
    [Header("Shooting Settings")]
    [Tooltip("Time before turret shoots after detection")] [SerializeField] private float _chargeTime;
    [Tooltip("Time Turret Waits before shooting agains")] [SerializeField] private float _coolDownTime;
    [Tooltip("Time between individual projectile shots")] [SerializeField] private float _shotDelay;
    [Tooltip("Number of projectiles fired per volley")] [SerializeField] private int _shotAmount;

    private Transform _projectileSpawn;
    private Transform _target;
    private PolygonCollider2D _visionCone;
    private bool _isShooting;
    private void Awake()
    {
        _projectileSpawn = GetComponentInChildren<Transform>();
        _visionCone = GetComponentInChildren<PolygonCollider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        #region VisionCone Setup
        //set up vision cone length and height
        Vector2[] points = new Vector2[3] {Vector2.zero, new Vector2(_visionConeRange, _visionConeHeight), new Vector2(_visionConeRange, -_visionConeHeight) };
        _visionCone.SetPath(0, points);
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        if(!_isShooting)
        {
            if(_target != null)
            {
                StartCoroutine(ProjectileAttack());
            }
        }
    }

    private void ShootProjectile()
    {
        TurretProjectile projectile = PoolManager.Instance.Spawn("TurretProjectile").GetComponent<TurretProjectile>();
        projectile.transform.position = _projectileSpawn.position;
        projectile.Direction = ((_target.transform.position - transform.position).normalized);
        projectile.Speed = _projectileVelocity;
        projectile.Shoot();
    }

    IEnumerator ProjectileAttack()
    {
        _isShooting = true;
        yield return new WaitForSeconds(_chargeTime);

        for (int i = 0; i < _shotAmount; i++)
        {
            ShootProjectile();
            yield return new WaitForSeconds(_shotDelay);
        }
        yield return new WaitForSeconds(_coolDownTime);
        _isShooting = false;
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _target = collision.gameObject.transform;
            Debug.Log(_target.name);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == _target?.gameObject)
            _target = null;
    }
}
