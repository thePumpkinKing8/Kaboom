using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardTurret : MonoBehaviour
{
    //public void Shoot()
    //{
    //    throw new System.NotImplementedException();
    //}

    // WILL REFRACTOR THESE TO NOT BE SO TERRIBLE

    [SerializeField]
    private Transform _shotSpawnPoint;

    [SerializeField]
    private GameObject _bulletPrefab;

    [SerializeField]
    private float _shotSpeed = 2f;

    [SerializeField]
    private float _shotDelay = 5f;

    private bool _currentlyShooting = false;

    private void Update()
    {
        if(_currentlyShooting == false)
        {
            StartCoroutine(WaitToShoot());
        }
    }

    private IEnumerator WaitToShoot()
    {
        _currentlyShooting = true;

        yield return new WaitForSeconds(_shotDelay);

        var bullet = Instantiate(_bulletPrefab, _shotSpawnPoint.position, _shotSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = _shotSpawnPoint.right * _shotSpeed;

        _currentlyShooting = false;
    }
}
