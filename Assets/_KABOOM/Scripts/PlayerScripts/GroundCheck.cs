using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private Transform _checkTransform;
    [SerializeField] private GameObject _checkGameObject;
    private PlayerSettings _settings;  
    public bool IsGrounded() => Physics2D.OverlapCircle(_checkTransform.position, _settings.groundCheckRadius, _settings.groundLayerMask);
}
