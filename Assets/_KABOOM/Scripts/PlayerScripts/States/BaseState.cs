
using UnityEngine;

public class BaseState : MonoBehaviour, IPlayerState
{
    private Rigidbody2D _rb;
    private PlayerActionsData _actions;
    public float Momentum { get; set; }
    private bool _stateActive;
    private GroundCheck _groundCheck;
    private float _horizontal;
    private PlayerSettings _settings;
    private ShootingState _shootingState;
    private FallingState _fallingState;
 
    private void Awake()
    {
        _actions = InputManager.Instance.ActionsData;
        _actions.PlayerMoveEvent.AddListener(HandleMovement);
        _actions.PlayerShootEvent.AddListener(PlayerShooting);
        _rb = GetComponent<Rigidbody2D>();
        _groundCheck = GetComponentInChildren<GroundCheck>();
        _shootingState = GetComponent<ShootingState>();
        _fallingState = GetComponent<FallingState>();
        _settings = GameManager.Instance.PlayerPhysicsSettings;
    }

    private void Start()
    {
        _rb.gravityScale = _settings.gravityScale;
        EnterState(); //this is always the starting state
    }
    //in this state the player is grounded and can walk
    public void EnterState(float momentum = 0)
    {
        _stateActive = true;
        Momentum = momentum;
    }

    private void Update()
    {
        if(_stateActive)
        {
            if (!_groundCheck.IsGrounded(_settings.groundCheckRadius, _settings.groundLayerMask))
            {
                ExitState(_fallingState);
                //go to falling state
            }
        }     
    }

    private void FixedUpdate()
    {
        if (_stateActive)
        {
            HandleMomentum();
            _rb.velocity = new Vector2(_horizontal * _settings.movementSpeed + Momentum, _rb.velocity.y);
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

        Momentum = (Mathf.Abs(Momentum) - _settings.playerFriction); //reduce the absolute value of the players momentum 

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
