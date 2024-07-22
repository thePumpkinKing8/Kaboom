using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    private PlayerInputActions _input;
    [SerializeField] private PlayerActionsData _actions;
    public PlayerActionsData ActionsData { get { return _actions; } private set { _actions = value; } }


    
    private void OnEnable()
    { 
        if (_input == null)
        {
             _input = new PlayerInputActions();
             _input.Player.Move.performed += (val) => _actions.HandlePlayerMovement(val.ReadValue<Vector2>());
             _input.Player.Jump.performed += (val) => _actions.HandlePlayerJump();
             _input.Player.Jump.canceled += (val) => _actions.HandleJumpCancel();
             _input.Player.Aim.performed += (val) => _actions.HandlePlayerAim(val.ReadValue<Vector2>(), val.control.device is Mouse);
             _input.Player.Shoot.performed += (val) => _actions.HandlePlayerShoot();
             _input.Player.Shoot.canceled += (val) => _actions.HandleShootCancel();
        }
         _input.Enable();
    }

}
