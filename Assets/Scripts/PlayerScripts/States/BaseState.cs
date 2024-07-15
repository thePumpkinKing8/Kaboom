
using UnityEngine;


/// <summary>
/// Base class for player states.
/// </summary>
public abstract class BaseState
{
    public string name;
    protected internal PlayerController player;
    protected InputController input;
    protected PlayerSettings settings;
    protected BaseState(string name, PlayerController player)
    {
        this.name = name;
        this.player = player;
        input = player.inputController;
        settings = player.settings;
    }

    #region Cached Properties
    /* left over from goofland
    protected static readonly int Walking = Animator.StringToHash("Walking");
    protected static readonly int Running = Animator.StringToHash("Running");
    protected static readonly int Jumping = Animator.StringToHash("Jumping");
    protected static readonly int Falling = Animator.StringToHash("Falling");
    protected static readonly int Gliding = Animator.StringToHash("Gliding");
    protected internal static readonly int Attacking = Animator.StringToHash("Attacking");
    */
    #endregion

    protected void ChangeState(BaseState state) => player.ChangeState(state); 
    public virtual void EnterState() { }

    public virtual void HandleInput()
    {
 
    }

    public virtual void UpdateState()
    {
        
    }
    /// <summary>
    /// used to handle movement that we want to happen on the fixed update step
    /// </summary>
    public virtual void HandleMovement()
    {

    }

    public virtual void ExitState() { }


}
