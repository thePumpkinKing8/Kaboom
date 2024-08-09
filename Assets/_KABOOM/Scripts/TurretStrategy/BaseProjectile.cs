using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : PoolObject
{
    [HideInInspector] public Vector2 Direction;
    [SerializeField] private float _speed;
    public float Speed { get { return _speed; } set { _speed = value; } }

    public int damage;

    private void Awake()
    {
        Shoot();
    }
    // Start is called before the first frame update
    public void Shoot()
    {
        GetComponent<Rigidbody2D>().velocity = Direction * _speed;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.gameObject.layer == ~LayerMask.NameToLayer("Projectile"))
        {
            OnDeSpawn();
        } 
    }
}
