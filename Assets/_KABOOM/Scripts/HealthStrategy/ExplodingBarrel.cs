using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class ExplodingBarrel :MonoBehaviour, IDestructable
{
    [SerializeField] protected float _explosionForce;
    [SerializeField] protected float _explosionRadius = 10f;
    private void Awake()
    {
    }

    private void Start()
    {
        
    }

    public void Health()
    {
        Explode();
    }

    public void Explode()
    {
        //_explosionTrigger.SetActive(true); // Sets the trigger as active
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, _explosionRadius, LayerMask.NameToLayer("Player"));
        if(playerCollider)
        {
            playerCollider.gameObject.GetComponent<Rigidbody2D>().AddForce((playerCollider.transform.position - transform.position).normalized * _explosionForce);
        }

        RaycastHit2D[] circle = Physics2D.CircleCastAll(transform.position, _explosionRadius, transform.position,0);
      

        foreach(RaycastHit2D hit in circle)
        {
            if(hit.collider.gameObject == gameObject)
            {
                //do nothing
            }

            else if(hit.collider.GetComponent<BreakableTile>())
                hit.collider.GetComponent<BreakableTile>()?.BreakTile(hit.point);

            else if(hit.collider.GetComponent<IDestructable>() != null)
                hit.collider.GetComponent<IDestructable>()?.ObjectDestroyed();
        }


        // Some logic for explosions/event to trigger explosion logic here

        PoolObject obj = PoolManager.Instance.Spawn("BigExplosion");
        LevelManager.Instance.EventData.BarrelExplosionEvent.Invoke("Barrel");
        obj.transform.position = transform.position;
    }


    public void ObjectDestroyed()
    {
        Explode();
        float explosionDelay = 0.1f;
        Destroy(this.gameObject, explosionDelay);
    }
}
