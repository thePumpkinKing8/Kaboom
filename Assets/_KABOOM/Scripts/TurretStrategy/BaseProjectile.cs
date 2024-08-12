using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BaseProjectile : PoolObject
{
    [HideInInspector] public Vector2 Direction;
    [SerializeField] private float _speed;
    public float Speed { get { return _speed; } set { _speed = value; } }

    public int damage;

    public float Lifetime = 10f;

    private LayerMask _groundLayer;

    private void Start()
    {
        //Shoot();
    }
    // Start is called before the first frame update
    public virtual void Shoot()
    {
        GetComponent<Rigidbody2D>().velocity = Direction * _speed;
        StartCoroutine(DespawnAfterLifetime());
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {

            Vector2 Direction = -collision.relativeVelocity.normalized;
            //collision.gameObject.GetComponent<PlayerController>().OnHit(damage, direction);
            OnDeSpawn();
        }


        else if (collision.collider.gameObject.layer == ~LayerMask.NameToLayer("Projectile"))
        {

            Vector2 Direction = -collision.relativeVelocity.normalized;
            //collision.gameObject.GetComponent<PlayerController>().OnHit(damage, direction);
            OnDeSpawn();
        }
    }


    IEnumerator DespawnAfterLifetime()
    {
        yield return new WaitForSeconds(Lifetime);
        OnDeSpawn();
    }
}
