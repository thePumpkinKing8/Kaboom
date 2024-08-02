
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
        EnterState();
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

    // used to handle movement that we want to happen on the fixed update step
    public void HandleMovement(Vector2 move)
    {
        _horizontal = move.x;
    }

    public void HandleMomentum()
    {
        float sign = Mathf.Sign(Momentum);
        
        Momentum = (Mathf.Abs(Momentum) - _settings.playerFriction);
        

        if (Momentum <= 0)
            Momentum = 0;
        else
            Momentum *= sign;
    }

    private void PlayerShooting()
    {
        ExitState(_shootingState);
        //change state to shooting state
    }
    public void ExitState(IPlayerState state) 
    {
        _stateActive = false;
        state.EnterState(Momentum);
        
    }


}
