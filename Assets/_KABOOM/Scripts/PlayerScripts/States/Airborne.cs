 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airborne : BaseState
{
    protected Airborne(string name, PlayerController player) : base(name, player)
    {
    }
 

    public override void EnterState()
    {
        base.EnterState();
        player.groundCheck.Translate(new Vector3(0,-settings.jumpBufferOffset, 0));
    }
    public override void HandleInput()
    {
        base.HandleInput();

    }

    public override void UpdateState()
    {
        base.UpdateState();

        Move();
        //HandleFallSpeed();
        if (player.GetCurrentState() != player.fallingState && player._rb.velocity.y < -player.settings.fallCheck)
        {
            player.ChangeState(player.fallingState);
        }
            
    }

    public override void HandleMovement()
    {
        base.HandleMovement();
        if (Mathf.Abs(player.xMomentum) > 0)
            HandleMomentum();
    }

    private void Move()
    {
        player._rb.velocity = new Vector2((input.MoveInput.x * settings.airSpeed) + player.xMomentum, player._rb.velocity.y);
    }
  
    //reduces xMomentum by the settings drag coeffecient
    protected void HandleMomentum()
    {
        float sign = Mathf.Sign(player.xMomentum);
        player.xMomentum = (Mathf.Abs(player.xMomentum) - settings.playerDrag);
        if (player.xMomentum <= 0)
        {
            player.xMomentum = 0;
            Debug.Log("?");
        }
           
        else
            player.xMomentum *= sign;
    }

    public override void ExitState()
    {
        base.ExitState();
        player.groundCheck.Translate(new Vector3(0, -settings.jumpBufferOffset, 0));
    }


}
