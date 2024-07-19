using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : BaseState
{
    protected Grounded(string name, PlayerController player) : base(name, player) 
    {
    }

    private float _coyoteTimer = 0;
    public override void EnterState()
    {
        base.EnterState();
        //applies any leftover momentum from when the player was airborne
        if(player.momentum.x != 0 )
        {
            player._rb.velocity = new Vector2(player.momentum.x, 0);
            player.momentum = Vector2.zero;
        }
        
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

  

    private void CoyoteTime()
    {
        _coyoteTimer += Time.deltaTime;
        if(_coyoteTimer >= settings.coyoteTime)
        {
            player.ChangeState(player.fallingState);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
        _coyoteTimer = 0;
    }

}
