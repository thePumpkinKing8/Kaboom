using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatTurret : BaseShoot, IShoot
{
    private void Start()
    {
        this.CurrentShotType = this; // Sets this as the shot type for the turret in question. Will not use the interface otherwise.
    }

    public void Shoot()
    {
        // Set variables for this type of turret
        this._shotSpeed = 2.5f;

        this._shotDelay = 4f;

        this._bulletLifetime = 8f;
    }
}
