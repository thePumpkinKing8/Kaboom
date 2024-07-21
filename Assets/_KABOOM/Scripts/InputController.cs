using System;
using System.Collections;
using System.Collections.Generic;
using _KABOOM.Scripts.PlayerScripts;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputController : MonoBehaviour
{
    private InputActionAsset _inputActions;
    private InputActionMap _inputMap;
    private PlayerInputActions _input;
    private PlayerController _playerController;
    private TestPlayerController _testPlayerController;
    public Vector2 MoveInput { get; private set; }

    public bool IsJump { get; set; }

    public bool IsShoot { get; private set; }

    public bool IsStart { get; private set; }


    [HideInInspector] public bool IsFalling;

    [HideInInspector] public bool IsMoving;

    [HideInInspector] public bool IsIdle;

    [HideInInspector] public bool IsJumping;

    [HideInInspector] public bool JumpHeld;
    void Awake()
    {
       // _input = GetComponent<PlayerInput>();
        _playerController = GetComponent<PlayerController>();
        _testPlayerController = GetComponent<TestPlayerController>();
        // _inputActions = _input.actions;
        //_inputMap = _inputActions.FindActionMap("Player");
        //set which controller will control this player

    }

    private void OnEnable()
    {
        if (_input == null)
        {
            _input = new PlayerInputActions();
            _input.Player.Move.performed += (val) => _testPlayerController.HandleMovement(val.ReadValue<Vector2>());
            _input.Player.Jump.performed += (val) => _testPlayerController.HandleJump();
            _input.Player.Aim.performed += (val) => _testPlayerController.HandleDirection(val.ReadValue<Vector2>());
            _input.Player.Shoot.performed += (val) => _testPlayerController.HandleShoot();
            _input.Player.Shoot.canceled += (val) => _testPlayerController.StopShooting();
        }


        _input.Enable();
    }


    // Update is called once per frame
    /// <summary>
    /// This is not a good way to set up your input controller;
    /// it couples you to the input system.
    /// Instead you should be calling functions
    /// ideally ones in a scriptable object like an event channel that send out events that can be used.
    /// You want to divorce yourself from the input system so any system 
    /// </summary>
    void Update()
    {
        // MoveInput = _inputMap.FindAction("Move").ReadValue<Vector2>();
        //
        // IsJump = _inputMap.FindAction("Jump").triggered;
        // JumpHeld = _inputActions.FindAction("Jump").IsPressed(); //This is horribly slow as it involves looking things up every call
        //
        // IsShoot = _inputMap.FindAction("Shoot").IsPressed();
        //
        // IsStart = _inputMap.FindAction("Pause").triggered;
        //
        // //state checks
        // IsFalling = _playerController._rb.velocity.y < -_playerController.settings.fallCheck && !_playerController.IsGrounded();
        //
        // IsMoving = MoveInput != Vector2.zero;
        //
        // IsIdle = !IsMoving && !IsFalling;
        // IsStart = _inputMap.FindAction("Start").triggered;
       // Debug.Log(IsMoving);
    }
}
