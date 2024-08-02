using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private Transform _checkTransform;
    private void Awake()
    {
        _checkTransform = GetComponent<Transform>();
    }
    public bool IsGrounded(float radius, LayerMask layer) => Physics2D.OverlapCircle(_checkTransform.position, radius, layer);
}
