using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardTurret : BaseShoot, IShoot
{
    public void Shoot()
    {
        throw new System.NotImplementedException();
    }

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

    private void Start()
    {
        this.CurrentShotType = this; // Sets this as the shot type for the turret in question. Will not use the interface otherwise.

        // Set variables for this type of turret
        this._shotSpeed = 5f;
    }
}
