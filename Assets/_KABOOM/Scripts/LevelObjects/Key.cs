using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Collectibles
{
    protected override void Collect()
    {
        LevelManager.Instance.EventData.KeyCollectedEvent.Invoke("KeyCollected");
        base.Collect(); 
    }
}
