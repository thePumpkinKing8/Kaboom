using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedyTurret : BaseShoot, IShoot
{
    public void Shoot()
    {
        this._shotSpeed = 50f;

        this._shotDelay = 2f;

        this._bulletLifetime = 3f;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.CurrentShotType = this;
    }

}
