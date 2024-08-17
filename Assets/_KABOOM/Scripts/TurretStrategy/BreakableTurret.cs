using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableTurret : StandardTurret, IDestructable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ObjectDestroyed()
    {
        LevelManager.Instance.EventData.TurretDestroyedEvent.Invoke("Explosion");
    }
}
