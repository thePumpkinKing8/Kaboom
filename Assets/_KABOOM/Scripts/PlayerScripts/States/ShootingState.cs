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
        _laser = GetComponentInChildren<PlayerLaser>();
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
            _rb.AddForce(_laser.AddForce(_settings.shootingForce));
        }
    }

    // used to handle movement that we want to happen on the fixed update step
    public void HandleMovement(Vector2 move)
    {
        
    }

    public void HandleMomentum()
    {
        
    }

    private void StopShooting()
    {
        ExitState(_groundCheck.IsGrounded(_settings.groundCheckRadius, _settings.groundLayerMask) ? _baseState : _fallingState);
        //change state to shooting state
    }

    public void ExitState(IPlayerState state)
    {
        Momentum = _rb.velocity.x;
        _stateActive = false;
        state.EnterState(Momentum);
    }
}
