using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerState 
{
    void EnterState();
    void HandleMovement(Vector2 movement);
    void HandleMomentum();
    void ExitState();
}
