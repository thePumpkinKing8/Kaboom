
using UnityEngine;
/*

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
    


    

    protected void ChangeState(BaseState state) => player.ChangeState(state); 
    public virtual void EnterState() { }

    //handled on the update step
    public virtual void HandleInput()
    {
        if (input.IsShoot)
            player.ChangeState(player.shootingState);
    }


    //handled on the update step
    public virtual void UpdateState()
    {
        if(Mathf.Abs(player.rb.velocity.x) > settings.maxVelocity)
        {
            player.rb.velocity = new Vector2(Mathf.Sign(player.rb.velocity.x) * settings.maxVelocity, player.rb.velocity.y);
        }

        if (Mathf.Abs(player.rb.velocity.y) > settings.maxVelocity)
        {
            player.rb.velocity = new Vector2(player.rb.velocity.x, Mathf.Sign(player.rb.velocity.y) *  settings.maxVelocity);
        }
    }
    /// <summary>
    /// used to handle movement that we want to happen on the fixed update step
    /// </summary>
    public virtual void HandleMovement()
    {

    }

    public virtual void ExitState() 
    {
    
    }


}
*/