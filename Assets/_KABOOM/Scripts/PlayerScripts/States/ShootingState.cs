using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingState : BaseState
{
    public ShootingState(PlayerController player) : base("ShootingState", player)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
    }
    public override void HandleInput()
    {
        base.HandleInput();
        if(player.IsGrounded())
        {
            player._rb.velocity = new Vector2(input.MoveInput.x * player.settings.movementSpeed, player._rb.velocity.y);
        }
        else
        {
            player._rb.velocity = GetAngle();
        }
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(!input.IsShoot)
        {
            if (player.IsGrounded())
                player.ChangeState(player.idleState);
            else
                player.ChangeState(player.fallingState);
        }
    }

    private Vector2 GetAngle()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 _direction = mousePosition - player.transform.position;
        float angle = Mathf.Atan2(_direction.y, _direction.x);

        Vector2 direction = (new Vector2(-(settings.shootingForce * Mathf.Cos(angle)), -(settings.shootingForce * Mathf.Sin(angle))));
        return direction;
    }

    public override void ExitState()
    {
        base.ExitState();   
        //saves the players momentum in the x axis
        player.momentum = new Vector2(player._rb.velocity.x, 0);
    }

}
