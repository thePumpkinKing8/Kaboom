using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    protected PlayerActionsData _actions;
    protected virtual void Awake()
    {
        _actions = GetComponent<PlayerActionsData>();
    }

    protected virtual void EnterState()
    {

    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    protected virtual void ExitState(PlayerState state)
    {

    }
}
