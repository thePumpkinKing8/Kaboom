
using UnityEngine;

public class BaseState : MonoBehaviour, IPlayerState
{
    public Rigidbody2D Rb;
    private PlayerActionsData _actions;
    private float _momentum;
    private bool _stateActive;
    private GroundCheck _groundCheck;
    private float _horizontal;
    [SerializeField] private PlayerSettings _settings;
    private void Awake()
    {
        _actions = InputManager.Instance.ActionsData;
        _actions.PlayerMoveEvent.AddListener(HandleMovement);
        _actions.PlayerShootEvent.AddListener(PlayerShooting);
        Rb = GetComponent<Rigidbody2D>();
        _groundCheck = GetComponentInChildren<GroundCheck>();
    }
    //in this state the player is grounded and can walk
    public void EnterState()
    {

    }

    private void Update()
    {
        if(_stateActive)
        {
            HandleMomentum();
            if (!_groundCheck.IsGrounded())
            {
                ExitState();
                //go to falling state
            }
        }     
    }

    private void FixedUpdate()
    {
        if (_stateActive)
        {
            Rb.velocity = new Vector2(_horizontal * _settings.movementSpeed + _momentum, Rb.velocity.y);
        }
    }

    // used to handle movement that we want to happen on the fixed update step
    public void HandleMovement(Vector2 move)
    {
        _horizontal = move.x;
    }

    public void HandleMomentum()
    {
        float sign = Mathf.Sign(_momentum);
        if (_groundCheck.IsGrounded())
            _momentum = (Mathf.Abs(_momentum) - _settings.playerFriction);
        else
            _momentum = (Mathf.Abs(_momentum) - _settings.playerDrag);

        if (_momentum <= 0)
            _momentum = 0;
        else
            _momentum *= sign;
    }

    private void PlayerShooting()
    {
        ExitState();
        //change state to shooting state
    }
    public void ExitState() 
    {
        _stateActive = false;
    }


}
