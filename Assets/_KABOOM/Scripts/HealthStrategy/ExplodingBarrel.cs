using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class ExplodingBarrel :MonoBehaviour, IDestructable
{
    [SerializeField] protected float _explosionForce;
    [SerializeField] protected float _explosionRadius;
    private void Awake()
    {
    }

    private void Start()
    {
        
    }

    public void Health()
    {
        Die();
    }

    public void Die()
    {
        Debug.Log("explode");
        //_explosionTrigger.SetActive(true); // Sets the trigger as active
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, 20f, LayerMask.NameToLayer("Player"));
        if(playerCollider)
        {
            playerCollider.gameObject.GetComponent<Rigidbody2D>().AddForce((playerCollider.transform.position - transform.position).normalized * _explosionForce);
        }

        RaycastHit2D[] circle = Physics2D.CircleCastAll(transform.position, 20f, Vector2.zero,LayerMask.NameToLayer("Breakable"));
      

        foreach(RaycastHit2D hit in circle)
        {
            hit.collider.GetComponent<BreakableTile>()?.BreakTile(hit.point);
        }

       
        // Some logic for explosions/event to trigger explosion logic here

        float explosionDelay = 0.1f; // Makes sure the object doesn't get destroyed before executing all its logic

        Destroy(this.gameObject, explosionDelay);
    }


    public void ObjectDestroyed()
    {
        Explode();
        float explosionDelay = 0.1f;
        Destroy(this.gameObject, explosionDelay);
    }
}
