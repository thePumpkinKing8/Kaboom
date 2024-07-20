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
        

    }

    public override void HandleInput()
    {
        base.HandleInput();

        if (player.IsGrounded())
            player.ChangeState(player._rb.velocity.x != 0 ? player.walkingState : player.idleState);
    }

    public override void ExitState()
    {
        base.ExitState();

        //player.anim.SetBool("Falling", false); 
    }
}
