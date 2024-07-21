using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Transform groundCheck;
    [Tooltip("temorary")]
    public GameObject gun;

    private Direction _lastDirection;
    [HideInInspector] public Direction lastDirection 
    { 
        get
        {
            return _lastDirection;
        }
        
        set 
        {
            _lastDirection = value; 
        } 
    }

    private float _currentHealth;

    [HideInInspector] public bool grounded = true;
    //leftover momentum in the xAxis 
    [HideInInspector] public float xMomentum = 0;
    
    

    public PlayerSettings settings;
    [HideInInspector] public Rigidbody2D _rb;
    [HideInInspector] public InputController inputController;
    [HideInInspector] public Animator anim;

    //state machine variables
    #region StateMachine
    private BaseState _currentState;

    //states
    [HideInInspector] public IdleState idleState;
    [HideInInspector] public WalkingState walkingState;
    [HideInInspector] public JumpState jumpState;
    [HideInInspector] public FallingState fallingState;
    [HideInInspector] public HitState hitState;
    [HideInInspector] public ShootingState shootingState;
    #endregion

    //GameEvents
    #region Events
    /*
    [Header("Events")]
     public GameEvent jumpEvent;
     public GameEvent hurtEvent;
    */
    #endregion

    //SFX
    #region Sound
    [Header("SFX")]
    public AudioClip jumpSFX;
    public AudioClip hurtSFX;
    public AudioClip shootSFX;
    public AudioClip dashSFX;
    public AudioClip compressSFX;
    public AudioClip shieldBlockSFX;
    public AudioClip shieldUpSFX;
    public AudioClip dieSFX;
    #endregion

    private void Awake()
    {

        _rb = GetComponent<Rigidbody2D>();
        inputController = GetComponent<InputController>();
        anim = GetComponent<Animator>();

        //set player health
        _currentHealth = settings.maxHealth;

        lastDirection = Direction.Right;

        _rb.gravityScale = settings.gravityScale;

        // set up player states
        #region StateSetUp
        idleState = new IdleState(this);
        walkingState = new WalkingState(this);
        fallingState = new FallingState(this);
        jumpState = new JumpState(this);
        hitState = new HitState(this);
        shootingState = new ShootingState(this);
        #endregion
        ChangeState(idleState);     
    }

    void Update()
    {
        FlipPlayer();
        _currentState.UpdateState();
        _currentState.HandleInput();
    }

    private void FixedUpdate()
    {
        //if (!GameManager.Instance.Pause)
            _currentState.HandleMovement();

        if(!grounded)
        {
            // Limits horizontal speed while in the air
            _rb.velocity = new Vector2(_rb.velocity.x * .5f, _rb.velocity.y);
        }
    }

    public void ChangeState(BaseState state)
    {
       _currentState?.ExitState();
        _currentState = state;
        _currentState?.EnterState();

       // Debug.Log(_currentState.name);
    }

    public BaseState GetCurrentState() => _currentState;



    private void FlipPlayer()
    {
        var size = transform.localScale;
        Direction direction = GetPlayerDirection();
        if (lastDirection != GetPlayerDirection())
        {
            lastDirection = GetPlayerDirection();
            FlipPlayer();
        }

        transform.localScale = new Vector3(direction == Direction.Right ? 1 : -1 * Mathf.Abs(size.x),size.y,size.z); 
    }



    private Direction GetPlayerDirection()
    {
        return inputController.MoveInput.x switch
        {
            > 0 => Direction.Right,
            < 0 => Direction.Left,
            _ => lastDirection,
        };
    }


    //returns true if player is ontop of an object with the ground layer
    public bool IsGrounded() => Physics2D.OverlapCircle(groundCheck.position, settings.groundCheckRadius, settings.groundLayerMask);
}

public enum Direction
{ 
    Left,
    Right
}


