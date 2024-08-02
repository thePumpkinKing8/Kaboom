
using UnityEngine;

public class BaseState : MonoBehaviour, IPlayerState
{
    public Rigidbody2D Rb;
    private PlayerActionsData _actions;
    public float Momentum { get; set; }
    private bool _stateActive;
    private GroundCheck _groundCheck;
    private float _horizontal;
    [SerializeField] private PlayerSettings _settings;
    private ShootingState _shootingState;
    private FallingState _fallingState;
 
    private void Awake()
    {
        _actions = InputManager.Instance.ActionsData;
        _actions.PlayerMoveEvent.AddListener(HandleMovement);
        _actions.PlayerShootEvent.AddListener(PlayerShooting);
        Rb = GetComponent<Rigidbody2D>();
        _groundCheck = GetComponentInChildren<GroundCheck>();
        _shootingState = GetComponent<ShootingState>();
        _fallingState = GetComponent<FallingState>();
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
            HandleMomentum();
            if (!_groundCheck.IsGrounded())
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
            Rb.velocity = new Vector2(_horizontal * _settings.movementSpeed + Momentum, Rb.velocity.y);
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
