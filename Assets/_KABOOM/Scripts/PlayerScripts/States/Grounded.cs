using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : BaseState
{
    protected Grounded(string name, PlayerController player) : base(name, player) 
    {
    }

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
            player.ChangeState(player.fallingState);

        if(input.IsJump)
            ChangeState(player.jumpState);        
    }

}
