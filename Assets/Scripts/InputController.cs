using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputController : MonoBehaviour
{
    private InputActionAsset _inputActions;
    private InputActionMap _inputMap;
    private PlayerInput input;
    private PlayerController playerController;  
    public Vector2 MoveInput { get; private set; }

    public bool IsJump { get; set; }

    public bool IsShoot { get; private set; }

    public bool IsStart { get; private set; }

    [HideInInspector] public bool IsFalling;

    [HideInInspector] public bool IsMoving;

    [HideInInspector] public bool IsIdle;

    [HideInInspector] public bool IsJumping;
    void Awake()
    {
        input = GetComponent<PlayerInput>();
        playerController = GetComponent<PlayerController>();
        _inputActions = input.actions;
        _inputMap = _inputActions.FindActionMap("Player");
        //set which controller will control this player

    }


    // Update is called once per frame
    void Update()
    {
        MoveInput = _inputMap.FindAction("Move").ReadValue<Vector2>();

        IsJump = _inputMap.FindAction("Jump").triggered;

        IsShoot = _inputMap.FindAction("Shoot").triggered;

        //state checks
        IsFalling = playerController._rb.velocity.y < -0.2f && !playerController.IsGrounded();

        IsMoving = MoveInput != Vector2.zero;

        IsIdle = !IsMoving && !IsFalling;
        // IsStart = _inputMap.FindAction("Start").triggered;
       // Debug.Log(IsMoving);
    }
}
