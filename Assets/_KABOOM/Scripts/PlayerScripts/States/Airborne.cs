using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airborne : BaseState
{
    protected Airborne(string name, PlayerController player) : base(name, player)
    {
    }

    private Vector2 playerVelocity;
    public override void HandleInput()
    {
        base.HandleInput();

    }

    public override void UpdateState()
    {
        base.UpdateState();

        Move();
        HandleFallSpeed();
        
    }

    private void Move()
    {
        player._rb.velocity = new Vector2(input.MoveInput.x * settings.movementSpeed, player._rb.velocity.y);
        
    }
    
    //reduces player velocity if the player is jumping and the jump button is pressed
    private void HandleFallSpeed()
    {
        if(player._rb.velocity.y > 0 && input.IsJump)
        {
            player._rb.velocity += Vector2.up * (Physics2D.gravity.y * (settings.lowJumpMultiplier - 1) * Time.deltaTime);
        }
    }

   
  
    
}
