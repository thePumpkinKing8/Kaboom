using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ShayProjectileSpawner : MonoBehaviour
{
    public ObjectPool<GameObject> _projectilePool;

    private void Start()
    {
       // _projectilePool = new ObjectPool<ShayTurretProjectile>();
    }

    //private GameObject CreateProjectile(GameObject projectile, Transform spawnPoint)
    //{
    //    Instantiate(projectile, spawnPoint.position, spawnPoint.rotation); // Instantiates a projectile at the spawn point (specified in ShayTurretProjectile.cs)

    //}
}
