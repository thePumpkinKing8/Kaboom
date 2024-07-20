 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airborne : BaseState
{
    protected Airborne(string name, PlayerController player) : base(name, player)
    {
    }

    private Vector2 playerVelocity;

    //leftover momentum in the xAxis 
    protected float _xMomentum;

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
        HandleFallSpeed();
        if (player.GetCurrentState() != player.fallingState && player._rb.velocity.y < -player.settings.fallCheck)
            player.ChangeState(player.fallingState);
    }

    private void Move()
    {
        player._rb.velocity = new Vector2((input.MoveInput.x * settings.airSpeed) + _xMomentum, player._rb.velocity.y);
        
        if (Mathf.Abs(_xMomentum) > 0)
            HandleMomentum();
    }
    
    //reduces player velocity if the player is jumping and the jump button is pressed
    private void HandleFallSpeed()
    {
        if(player._rb.velocity.y > 0 && input.IsJump)
        {
            player._rb.velocity += Vector2.up * (Physics2D.gravity.y * (settings.lowJumpMultiplier - 1) * Time.deltaTime);
        }
    }

    //reduces xMomentum by the settings drag coeffecient
    private void HandleMomentum()
    {
        float sign = Mathf.Sign(_xMomentum);
        _xMomentum = (Mathf.Abs(_xMomentum) - settings.dragCoeffecient);
        if (_xMomentum <= 0)
            _xMomentum = 0;
        else
            _xMomentum *= sign;
    }

    public override void ExitState()
    {
        base.ExitState();
        player.groundCheck.Translate(new Vector3(0, -settings.jumpBufferOffset, 0));
    }


}
