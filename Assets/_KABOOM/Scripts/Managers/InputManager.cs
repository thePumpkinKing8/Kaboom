using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    private PlayerInputActions _input;

    private PlayerController _playerController;
    private void OnEnable()
    {
        if(_input == null)
        {
            private void OnEnable()
            {
                if (_input == null)
                {
                    _input = new PlayerInputActions();
                    _input.Player.Move.performed += (val) => _layerController.HandleMovement(val.ReadValue<Vector2>());
                    _input.Player.Jump.performed += (val) => _playerController.HandleJump();
                    _input.Player.Jump.canceled += (val) => _playerController.CancelJump();
                    _input.Player.Aim.performed += (val) => _playerController.HandleDirection(val.ReadValue<Vector2>());
                    _input.Player.Shoot.performed += (val) => _playerController.HandleShoot();
                    _input.Player.Shoot.canceled += (val) => _playerController.StopShooting();
                }


                _input.Enable();
            }
        }
    }
}
