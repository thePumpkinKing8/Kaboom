using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StrategyFastBounce : BounceBase, IBounce
{
    void Start()
    {
       // Debug.Log("FastBounce");
        this.bounceType = this; //This is essential! Without it, anything inheriting from the BounceBase won't actually use the interface function that is being setup here 
        
        //Specifies direction and speed of the component, forming the behaviour modifications of the Strategy pattern. 
        direction = new Vector3(0, -1, 0); 
        speed = 0.001f; //This would probably be better with a get/set or a function, but this is faster atm 

    }
    
    public void Bounce()
    { 
        Debug.Log("fast Bounce");
        speed = 0.1f; //Directly changes the speed variable in the BounceBase. This would be better done with a private variable and get/set, or a SetSpeed() function. 
    }

}
