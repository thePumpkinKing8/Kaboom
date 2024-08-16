using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHealth : BaseHealth, IHealth
{
    void Start()
    {
        this.CurrentHealthType = this;
    }

    public void Health()
    {
        Die();
    }

    public void Die()
    {
        Destroy(this.gameObject); // Destroy the turret
    }

    // Needs the player laser in one of these
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Projectile":

                TakeDamage(_damageScriptableObject.TurretDamage); // Take the amount of damage the turrets deal
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "ExplosionTrigger":

                Die();
                break;
        }
    }
}
