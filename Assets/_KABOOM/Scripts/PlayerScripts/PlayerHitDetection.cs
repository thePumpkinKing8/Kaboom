using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitDetection : PlayerHealth
{
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BaseProjectile>())
        {
            LevelManager.Instance.EventData.PlayerHitEvent.Invoke("PlayerHit");
            GetHit(collision.gameObject.GetComponent<BaseProjectile>().Damage);
            GetComponent<Rigidbody2D>().AddForce(Vector3.Reflect(GetComponent<Rigidbody2D>().velocity, collision.contacts[0].normal));
        }
    }

    public void GetHit(int damage)
    {
        TakeDamage(damage);
        
        Die();
    }
}
