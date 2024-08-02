using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingState : MonoBehaviour, IPlayerState
{
    private Rigidbody2D _rb;
    private PlayerActionsData _actions;
    public float Momentum { get; set; }
    private bool _stateActive;
    private GroundCheck _groundCheck;
    private float _horizontal;
    private PlayerSettings _settings;
    private FallingState _fallingState;
    private BaseState _baseState;
    private PlayerLaser _laser;
    private void Awake()
    {
        _actions = InputManager.Instance.ActionsData; 
        _actions.PlayerShootCancel.AddListener(StopShooting);
        _rb = GetComponent<Rigidbody2D>();
        _groundCheck = GetComponentInChildren<GroundCheck>();
        _baseState = GetComponent<BaseState>();
        _fallingState = GetComponent<FallingState>();
        _laser = GetComponentInChildren<PlayerLaser>(); //the players laser that has information on the direction the gun is facing
        _settings = GameManager.Instance.PlayerPhysicsSettings;
    }


    public void EnterState(float momentum = 0)
    {
        _stateActive = true;
        Momentum = momentum;
    }
    private void Update()
    {
        if(_stateActive)
        {
            _laser.Shoot();
        }
    }

    private void FixedUpdate()
    {
        if(_stateActive)
        {
            HandleMovement(_laser.ForceVector(_settings.shootingForce));
        }
    }

    //applies force to the player
    public void HandleMovement(Vector2 force)
    {
        _rb.AddForce(force);
    }

    public void HandleMomentum()
    {
        
    }

    private void StopShooting() 
    {
        ExitState(_groundCheck.IsGrounded(_settings.groundCheckRadius, _settings.groundLayerMask) ? _baseState : _fallingState);
    }

    //sets this state to inactive and activates the next state
    public void ExitState(IPlayerState state)
    {
        Momentum = _rb.velocity.x;
        _stateActive = false;
        state.EnterState(Momentum);
    }
}
