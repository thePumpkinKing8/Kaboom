using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class JumpState : Airborne
{
    public JumpState(PlayerController player) : base("JumpingState", player)
    {
    }

    public override void EnterState()
    {
        input.IsJumping = true;

        //player.anim.SetBool("Jumping", true);

        Jump();

       // AudioManager.Instance.PlayerPlay(player.jumpSFX);
    }

    private void Jump()
    {
        if (!player.IsGrounded()) return;

        //player.jumpEvent.Raise();
        player.rb.velocity = new Vector2(player.rb.velocity.x, player.settings.jumpHeight);
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if (input.IsFalling)
        {
            player.ChangeState(player.fallingState);
            return; 
        }
            
        if(!input.JumpHeld && input.IsJumping)
        {
            player.rb.velocity = new Vector2(player.rb.velocity.x, player.rb.velocity.y / settings.shortJumpSlowDown);
            input.IsJumping = false;
        }
    }

    public override void ExitState()
    {
       // player.anim.SetBool("Jumping", false);
        input.IsJumping = false;
    }
}
*/