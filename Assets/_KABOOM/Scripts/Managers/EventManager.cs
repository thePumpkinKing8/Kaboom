using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    public LevelEventData LevelEvents { get; private set; }

    private void OnEnable()
    {
        LevelEvents = new LevelEventData();
    }
    
    public void Explosion()
    {
        LevelEvents.ExplosionEvent.Invoke("Explosion");
    }

    public void KeyCollected()
    {
        LevelEvents.KeyCollectedEvent.Invoke("KeyCollected");
    }

    public void TurretShot()
    {
        LevelEvents.TurretFireEvent.Invoke("TurretShot");
    }

    public void WallDestroyed()
    {
        LevelEvents.WallDestroyedEvent.Invoke("WallDestruction");
    }
}
