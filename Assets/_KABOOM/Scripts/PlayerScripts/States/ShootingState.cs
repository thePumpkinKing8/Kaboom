using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingState : MonoBehaviour, IPlayerState
{
    public Rigidbody2D Rb;
    private PlayerActionsData _actions;
    public float Momentum { get; set; }
    private bool _stateActive;
    private GroundCheck _groundCheck;
    private float _horizontal;
    [SerializeField] private PlayerSettings _settings;
    private FallingState _fallingState;
    private BaseState _baseState;
    private PlayerLaser _laser;
    private void Awake()
    {
        _actions = InputManager.Instance.ActionsData;
        _actions.PlayerShootCancel.AddListener(StopShooting);
        Rb = GetComponent<Rigidbody2D>();
        _groundCheck = GetComponentInChildren<GroundCheck>();
        _baseState = GetComponent<BaseState>();
        _fallingState = GetComponent<FallingState>();
        _laser = GetComponentInChildren<PlayerLaser>();
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
            Rb.AddForce(_laser.AddForce(_settings.shootingForce));
        }
    }

    private void FixedUpdate()
    {
        if (_stateActive)
        {
            Rb.velocity = new Vector2(_horizontal * _settings.airSpeed + Momentum, Rb.velocity.y);
        }
    }

    // used to handle movement that we want to happen on the fixed update step
    public void HandleMovement(Vector2 move)
    {
        _horizontal = move.x;
    }

    public void HandleMomentum()
    {
        
    }

    private void StopShooting()
    {
        ExitState(_groundCheck.IsGrounded() ? _baseState : _fallingState);
        //change state to shooting state
    }

    public void ExitState(IPlayerState state)
    {
        Momentum = Rb.velocity.x;
        _stateActive = false;
    }
}
