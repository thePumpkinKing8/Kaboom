using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerState 
{
    public float Momentum { get; set; }

    void EnterState(float momentum = 0);
    void HandleMovement(Vector2 movement);
    void HandleMomentum();
    void ExitState(IPlayerState state);
}
