using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : Airborne
{
    public FallingState(PlayerController player) : base("FallingState",player) 
    {
    }


    public override void EnterState()
    {
        base.EnterState();
        //player.anim.SetBool("Falling", true);
        
        if(player.momentum != Vector2.zero)
        {
            _xMomentum = player.momentum.x;
        }
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if (player.IsGrounded())
            player.ChangeState(input.IsMoving ? player.walkingState : player.idleState);
    }

    public override void ExitState()
    {
        base.ExitState();
        if(Mathf.Abs(_xMomentum) > 0)
        {
            player.momentum = new Vector2(_xMomentum,0);
        }
        //player.anim.SetBool("Falling", false); 
    }
}
