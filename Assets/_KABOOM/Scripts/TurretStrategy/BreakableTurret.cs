using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableTurret : StandardTurret, IDestructable
{
   

    public void ObjectDestroyed()
    {
        LevelManager.Instance.EventData.TurretDestroyedEvent.Invoke("Explosion");
        Destroy(gameObject);
    }
}
