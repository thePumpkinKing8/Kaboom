using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public PlayerSettings settings;
    private PlayerActionsData _playerActions;

    private void Awake()
    {
        _playerActions = InputManager.Instance.ActionsData;

        _playerActions.PlayerMoveEvent.AddListener(HandleMovement);
        _playerActions.PlayerJumpEvent.AddListener(HandleJump);
        _playerActions.PlayerJumpCancel.AddListener(JumpCancel);
    }

    private void HandleMovement(Vector2 val)
    {

    }
    private void HandleJump()
    {

    }
    private void JumpCancel()
    {

    }



}


