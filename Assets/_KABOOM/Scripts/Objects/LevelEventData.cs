using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "LevelEventData", menuName = "SOs/LevelEventData")]
public class LevelEventData : ScriptableObject
{
    public UnityEvent<string> ExplosionEvent;
    public UnityEvent<string> KeyCollectedEvent;
    public UnityEvent<string> TurretFireEvent;
    public UnityEvent<string> WallDestroyedEvent;
}
