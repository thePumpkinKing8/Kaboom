using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardTurret : BaseShoot, IShoot
{


    //private void Update()
    ////{
    ////    if(_currentlyShooting == false)
    ////    {
    ////        StartCoroutine(WaitToShoot());
    ////    }
    //}

    //private IEnumerator WaitToShoot()
    //{
    //    //_currentlyShooting = true;

    //    //yield return new WaitForSeconds(_shotDelay);

    //    //var bullet = Instantiate(_bulletPrefab, _shotSpawnPoint.position, _shotSpawnPoint.rotation);
    //    //bullet.GetComponent<Rigidbody2D>().velocity = _shotSpawnPoint.right * _shotSpeed;

    //    //_currentlyShooting = false;
    //}

    public float Speed
    {
        get { return _shotSpeed; }
        set { Speed = value; }
    }

    private void Start()
    {
        this.CurrentShotType = this; // Sets this as the shot type for the turret in question. Will not use the interface otherwise.

        _shotSpeed = 0.1f;
    }
    public void Shoot()
    {
        // Set variables for this type of turret
        this._shotSpeed = 5f;

        this._shotDelay = 2f;
    }
}
