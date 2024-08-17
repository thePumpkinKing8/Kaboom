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
    public UnityEvent<string> NewLevelStartEvent;
    public UnityEvent ReloadLevelEvent;
    public UnityEvent LevelCompleteEvent;
    public UnityEvent<string> PlayerKilledEvent;
    public UnityEvent<float> OnHealthChangedEvent;
    public UnityEvent<string> TurretDestroyedEvent;
    public UnityEvent<string> BarrelExplosionEvent;
    public UnityEvent<string> PlayerHitEvent;
    public UnityEvent ToMenu;
}
