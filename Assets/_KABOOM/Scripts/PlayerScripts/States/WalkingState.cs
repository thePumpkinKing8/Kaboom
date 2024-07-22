using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingState : Grounded
{
    public WalkingState(PlayerController player) : base("WalkingState", player)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
       // player.anim.SetBool("Walking", true);
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if (player.rb.velocity.x == 0)
            player.ChangeState(player.idleState);
            
    }

    public override void UpdateState()
    {
        base.UpdateState();
        player.rb.velocity = new Vector2(input.MoveInput.x * player.settings.movementSpeed + player.xMomentum, player.rb.velocity.y);
    }

    public override void ExitState()
    {
        base.ExitState();
        //player.anim.SetBool("Walking", false);
    }
}
