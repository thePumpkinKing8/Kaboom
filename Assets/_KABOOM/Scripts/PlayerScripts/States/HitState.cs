using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : BaseState
{
    public HitState(PlayerController player) : base("HitState", player)
    {
    }
    public Vector2 direction;
    private float timer;

    public override void EnterState()
    {
        base.EnterState();
        player.rb.velocity = direction * settings.knockBackForce;

        if(player.rb.velocity.y <= 0)
        {
            player.rb.velocity = new Vector2(player.rb.velocity.x, 6);
        }
    }

    public override void UpdateState()
    {
        base.UpdateState();
        timer += Time.deltaTime;
        if(timer >= settings.hitTime)
        {
            ChangeState(input.IsFalling ? player.fallingState : player.idleState);
        }
    }
}
