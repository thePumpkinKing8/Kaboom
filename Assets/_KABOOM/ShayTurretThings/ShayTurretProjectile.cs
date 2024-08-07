using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ShayTurretProjectile : MonoBehaviour
{
    private Rigidbody2D _projectileRB; // This object's rb

    [SerializeField]
    private Transform _projectileSpawnPoint; // Empty object that is a child of the turret, positioned at the end of the barrel


    private void Awake()
    {
        // Get our rigid body
        _projectileRB = GetComponent<Rigidbody2D>();

        // Position the projectile
        this.transform.position = _projectileSpawnPoint.position;
        this.transform.rotation = _projectileSpawnPoint.rotation;
    }
    public void FireProjectile(float speed)
    {
        // Launch the projectile forwards
        _projectileRB.velocity = _projectileSpawnPoint.right * speed; // Set the speed in the turret strategy
    }

    public void DespawnProjectile()
    {
        // Should be called when the projectile's lifespan is up OR if the projectile hits something
        this.gameObject.SetActive(false);
    }
}
