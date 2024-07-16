using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Represents player settings for use in the game.
/// </summary>
[CreateAssetMenu(fileName = "Player", menuName = "Player/Settings")]
public class PlayerSettings : ScriptableObject
{
    [Header("Player Settings")]
    public float movementSpeed = 5f;
    public float jumpHeight = 10f;
    [Tooltip("lower the value lower the jump")]
    public float lowJumpMultiplier = 5f;
    public float maxHealth = 100f;
    public float invulnFrameTime = 1.5f;
    public float knockBackForce = 6f;
    [Tooltip("time player has no control after being hit")]
    public float hitTime = .25f;

    [Header("Shooting Settings")]
    public float shootingCooldown = .15f;


    [Header("GroundCheck Settings")]
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayerMask;

}
