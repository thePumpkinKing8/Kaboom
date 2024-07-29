using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public PlayerSettings Settings;
    private PlayerActionsData _playerActions;
    public Rigidbody2D Rb { get; private set; }
    private SpriteRenderer _sprite;
    [SerializeField] private Transform _groundCheck;
    private float _horizontal;
    [HideInInspector] public float XMomentum;
    private bool _isShooting;
    private void Awake()
    {
        _playerActions = InputManager.Instance.ActionsData;

        _playerActions.PlayerMoveEvent.AddListener(HandleMovement);

        InputManager.Instance.ActionsData.PlayerShootEvent.AddListener(StartShooting);
        InputManager.Instance.ActionsData.PlayerShootCancel.AddListener(StopShooting);

        Rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();

        Rb.gravityScale = Settings.gravityScale;
    }
    private void Update()
    {
        
        HandleMomentum();

        if (Mathf.Abs(Rb.velocity.x) > Settings.maxVelocity)
        {
            Rb.velocity = new Vector2(Mathf.Sign(Rb.velocity.x) * Settings.maxVelocity, Rb.velocity.y);
        }

        if (Mathf.Abs(Rb.velocity.y) > Settings.maxVelocity)
        {
            Rb.velocity = new Vector2(Rb.velocity.x, Mathf.Sign(Rb.velocity.y) * Settings.maxVelocity);
        }
    }

    private void FixedUpdate()
    {
        if (!_isShooting)
        {
            Rb.velocity = new Vector2(_horizontal * Settings.movementSpeed + XMomentum, Rb.velocity.y);
        }
        
    }

    private void StartShooting()
    {
        _isShooting = true;
    }

    private void StopShooting()
    {
        _isShooting = false;
    }

    private void HandleMovement(Vector2 val)
    {
        _horizontal = val.x;
    }
   

    private void HandleMomentum()
    {
        if (!_isShooting)
        {
            float sign = Mathf.Sign(XMomentum);
            if (IsGrounded())
                XMomentum = (Mathf.Abs(XMomentum) - Settings.playerFriction);
            else
                XMomentum = (Mathf.Abs(XMomentum) - Settings.playerDrag);

            if (XMomentum <= 0)
                XMomentum = 0;
            else
                XMomentum *= sign;
        }
        
    }
    public void AddForce(Vector2 force)
    {
        Rb.AddForce(force);
    }

    public void FlipPlayer(bool flip)
    {
        if (flip)
            GetComponent<SpriteRenderer>().flipX = true;
        else
            GetComponent<SpriteRenderer>().flipX = false;
    }

    //returns true if player is ontop of an object with the ground layer
    public bool IsGrounded() => Physics2D.OverlapCircle(_groundCheck.position, Settings.groundCheckRadius, Settings.groundLayerMask);
}


