using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlaceholder : MonoBehaviour
{
    private float _bulletLifetime = 5;
    public BaseShoot parent; 


    private void OnCollisionEnter2D(Collision2D collision)
    {
        parent.DespawnProjectile(this.gameObject)
    }
}
