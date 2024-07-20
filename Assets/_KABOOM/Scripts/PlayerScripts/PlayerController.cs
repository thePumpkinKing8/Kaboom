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

    private float _lastDirection;
    [HideInInspector] public float lastDirection 
    { 
        get
        {
            return _lastDirection;
        }
        
        set 
        {
            _lastDirection = (value > 0 ? 1 : -1); 
        } 
    }

    public float Horizontal 
    {
        get
        {
            return _horizontal;
        }

        //this is messy and may no longer be needed. clean up after prototype
        private set
        {
            if (value != 0)
            {
                _horizontal = value;
                if(lastDirection != value)
                {
                    lastDirection = value;
                    FlipPlayer();
                }
            }
            else
                _horizontal = value;
        } 
    }
    private float _horizontal;

    private float _currentHealth;

    [HideInInspector] public bool grounded = true;
    [HideInInspector] public Vector2 momentum 
    { 
        get
        {
            return _momentum;
        }
        set
        {
            _momentum = value;
        }
    }
    private Vector2 _momentum;
    

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
        //if (GameManager.Instance.player == null)
          //  GameManager.Instance.player = this;

        _rb = GetComponent<Rigidbody2D>();
        inputController = GetComponent<InputController>();
        anim = GetComponent<Animator>();

        //set player health
        _currentHealth = settings.maxHealth;

        lastDirection = 1;

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
        /*
        if(!GameManager.Instance.Pause)
        {
            Horizontal = InputManager.Move.ReadValue<Vector2>().x;
            _currentState.UpdateState();
            _currentState.HandleInput();
        }
        */
        Horizontal = inputController.MoveInput.x;
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
        Vector3 direction = GetPlayerDirection();

        transform.localScale = new Vector3(direction.x * Mathf.Abs(size.x),size.y,size.z); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            grounded = true;
        }
    }

    public void OnHit(float damage, Vector2 direction)
    {
        ChangeHealth(-damage);

        if (_currentHealth <= 0)
            Die();
        else
        {
            hitState.direction = direction;
            ChangeState(hitState);
           // AudioManager.Instance.PlayerPlay(hurtSFX);
        }
    }

    public void ChangeHealth(float health)
    {
        _currentHealth += health;
        //healthbar.ChangeHealthFill(_currentHealth);  //Adjusts healthbar based on players health
    }
    private void Die()
    {
        //player dies
       // AudioManager.Instance.PlayerPlay(dieSFX);
        anim.SetBool("Die", true);
        /*
        this.Wait(1.06f, () => { GameManager.Instance.PlayerDie(); });
        this.Wait(1.06f, () => {InputManager.Instance.enabled = false; });
        this.Wait(1.06f, () => { anim.SetBool("Die", false); });
        anim.SetBool("Die", true);
        */
    }

    private Vector3 GetPlayerDirection()
    {
        return inputController.MoveInput.x switch
        {
            > 0 => Vector3.right,
            < 0 => Vector3.left,
            _ => new Vector3(Mathf.Round(lastDirection),0,0),
        };
    }


    //returns true if player is ontop of an object with the ground layer
    public bool IsGrounded() => Physics2D.OverlapCircle(groundCheck.position, settings.groundCheckRadius, settings.groundLayerMask);
}


