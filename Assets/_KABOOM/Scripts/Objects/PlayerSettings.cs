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
    public float maxVelocity = 20f;
    public float maxHealth = 100f;
    public float invulnFrameTime = 1.5f;
    public float knockBackForce = 6f;
    [Tooltip("time player has no control after being hit")]
    public float hitTime = .25f;
    [Header("Jump/Physics Settings")]
    public float airSpeed = 5f;
    public float gravityScale = 5f;
    public float playerDrag = .2f;
    public float playerFriction = .6f;
    [Tooltip("downward velocity required to be considered falling")]
    public float fallCheck = .2f;
    

    [Header("Shooting Settings")]
    public float shootingForce = 1f;
    public float maxShootingForce = 5f;
    public float minShootingForce = 1f;
    


    [Header("GroundCheck Settings")]
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayerMask;

}
