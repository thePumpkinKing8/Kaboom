using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : MonoBehaviour, IPlayerState
{
    private Rigidbody2D _rb;
    private PlayerActionsData _actions;
    public float Momentum { get; set; }
    private bool _stateActive;
    private GroundCheck _groundCheck;
    private float _horizontal;
    private PlayerSettings _settings;
    private ShootingState _shootingState;
    private BaseState _baseState;
    private void Awake()
    {
        _actions = InputManager.Instance.ActionsData;
        _actions.PlayerMoveEvent.AddListener(HandleMovement);
        _actions.PlayerShootEvent.AddListener(PlayerShooting);
        _rb = GetComponent<Rigidbody2D>();
        _groundCheck = GetComponentInChildren<GroundCheck>();
        _shootingState = GetComponent<ShootingState>();
        _baseState = GetComponent<BaseState>();
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
            if (_groundCheck.IsGrounded(_settings.groundCheckRadius, _settings.groundLayerMask))
            {
                ExitState(_baseState);
            }
        }       
    }

    private void FixedUpdate()
    {
        if (_stateActive)
        {
            HandleMomentum();
            _rb.velocity = new Vector2(_horizontal * _settings.airSpeed + Momentum, _rb.velocity.y);
        }
    }

    // used to handle movement inputs
    public void HandleMovement(Vector2 move)
    {
        _horizontal = move.x;
    }

    //manually reduces the players momentum over time
    public void HandleMomentum()
    {
        float sign = Mathf.Sign(Momentum); //save whether momentum is in a positive or negative direction
       
        Momentum = (Mathf.Abs(Momentum) - _settings.playerDrag); //reduce the absolute value of the players momentum 

        if (Momentum <= 0) //sets the player momentum to zero if it becomes negative
            Momentum = 0;
        else
            Momentum *= sign; //reapply the saved sign to momentum
    }

    private void PlayerShooting()
    {
        ExitState(_shootingState);
        //change state to shooting state
    }

    //sets this state to inactive and activates the next state
    public void ExitState(IPlayerState state)
    {
        _stateActive = false;
        state.EnterState(Momentum);
    }
}
