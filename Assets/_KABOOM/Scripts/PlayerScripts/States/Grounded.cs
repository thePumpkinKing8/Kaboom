using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : BaseState
{
    protected Grounded(string name, PlayerController player) : base(name, player) 
    {
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
