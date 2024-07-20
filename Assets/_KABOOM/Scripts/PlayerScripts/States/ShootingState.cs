using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingState : BaseState
{
    public ShootingState(PlayerController player) : base("ShootingState", player)
    {
    }
    private LineRenderer _laser;

    private Camera _camera;

    private Vector3 _mousePos;

    public override void EnterState()
    {
        _laser = player.gun.GetComponentInChildren<LineRenderer>();
        _laser.enabled = true;
        _camera = Camera.main;
    }
    public override void HandleInput()
    {
        base.HandleInput();
        _mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(player.gun.transform.position, GetAngle(), Mathf.Infinity , ~LayerMask.NameToLayer("Player"));
        Debug.DrawLine(player.gun.transform.position, hit.point);
        HandleLaser(hit);
 
        
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(!input.IsShoot)
        {
            if (player.IsGrounded())
                player.ChangeState(player.walkingState);
            else
                player.ChangeState(player.fallingState);
        }
    }

    private Vector2 GetAngle()
    {

        Vector2 _direction = new Vector2(_mousePos.x, _mousePos.y) - new Vector2(player.transform.position.x, player.transform.position.y);

        float angle = Mathf.Atan2(_direction.y, _direction.x);

        Vector2 direction = (new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)));
        return direction;
    }

    private void HandleLaser(RaycastHit2D rayHit)
    {
        Vector2 hitLocation = rayHit.point;

        float shootingForce = settings.minShootingForce;

        Vector2 direction = GetAngle();
        

        _laser.SetPosition(0, player.gun.transform.position);
        if(rayHit.collider == null)
        {
            float aspect = (float)Screen.width / Screen.height;

            float worldHeight = _camera.orthographicSize * 2;

            float worldWidth = worldHeight * aspect;
            _laser.SetPosition(1, GetAngle() * worldWidth);
            shootingForce = settings.minShootingForce;
        }
        else
        {
            float distance = hitLocation.magnitude - player.transform.position.magnitude;
            _laser.SetPosition(1, hitLocation);

            float _dist = Vector2.Distance(player.transform.position, hitLocation);
            if (_dist != 0)
            {
                _dist = 1f / _dist;
                shootingForce = Mathf.Clamp((_dist / 100) * settings.maxShootingForce, settings.minShootingForce, settings.maxShootingForce);
            }
           
            else
                shootingForce = settings.maxShootingForce;
            
        }

        Vector2 force = new Vector2(-shootingForce * direction.x, -shootingForce * direction.y);


        if (player.IsGrounded())
        {
            player._rb.velocity = new Vector2(input.MoveInput.x * player.settings.movementSpeed, player._rb.velocity.y);

        }
        else
        {
            player._rb.AddForce(force);
        } 
    }

    public override void ExitState()
    {
        base.ExitState();
        //saves the players momentum in the x axis
        player.xMomentum = player._rb.velocity.x;
        _laser.enabled = false;
    }

}
