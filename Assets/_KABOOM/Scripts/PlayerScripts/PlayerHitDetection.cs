using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitDetection : PlayerHealth
{
    private void Die()
    {
        GetComponent<Rigidbody2D>().freezeRotation = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BaseProjectile>())
        {
            LevelManager.Instance.EventData.PlayerKilledEvent.Invoke("PlayerHit");
            TakeDamage(collision.gameObject.GetComponent<BaseProjectile>().Damage);
        }
    }
}
