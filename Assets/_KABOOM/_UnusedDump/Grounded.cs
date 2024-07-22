using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class Grounded : BaseState
{
    protected Grounded(string name, PlayerController player) : base(name, player) 
    {
    }

    private float _coyoteTimer = 0;
    public override void EnterState()
    {
        base.EnterState();
        
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if (input.IsFalling)
        {
            CoyoteTime();
        }

        if(input.IsJump)
            ChangeState(player.jumpState);        
    }

    

    public override void HandleMovement()
    {
        base.HandleMovement();
        if(Mathf.Abs(player.xMomentum) > 0)
            HandleMomentum();
    }



    private void CoyoteTime()
    {
        _coyoteTimer += Time.deltaTime;
        if(_coyoteTimer >= settings.coyoteTime)
        {
            player.ChangeState(player.fallingState);
        }
    }

    protected void HandleMomentum()
    {
        float sign = Mathf.Sign(player.xMomentum);
        player.xMomentum = (Mathf.Abs(player.xMomentum) - settings.playerFriction);
        if (player.xMomentum <= 0)
            player.xMomentum = 0;
        else
            player.xMomentum *= sign;
    }

    public override void ExitState()
    {
        base.ExitState();
        _coyoteTimer = 0;
    }

}
*/