using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayerActionsData", menuName = "SOs/PlayerActionsData", order = 0)]
public class PlayerActionsData : ScriptableObject
{
    public UnityEvent<Vector2> PlayerMoveEvent;
    public UnityEvent PlayerJumpEvent;
    public UnityEvent PlayerShootEvent;
    public UnityEvent PlayerJumpCancel;
    public UnityEvent PlayerShootCancel;
    public UnityEvent<Vector2,bool> PlayerAimEvent;

    public void HandlePlayerMovement(Vector2 movement) => PlayerMoveEvent?.Invoke(movement);
    public void HandlePlayerJump() => PlayerJumpEvent.Invoke();
    public void HandleJumpCancel() => PlayerJumpCancel.Invoke();
    public void HandlePlayerShoot() => PlayerShootEvent.Invoke();
    public void HandleShootCancel() => PlayerShootCancel.Invoke();
    public void HandlePlayerAim(Vector2 direction, bool isKBM) => PlayerAimEvent.Invoke(direction, isKBM);

    
}
