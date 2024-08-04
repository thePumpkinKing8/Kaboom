/*The key to using interfaces and inheritance together is to remember that:
anything in Start(), even if inherited, won't run unless actually added in the Inspector. 
Use this to control what code you actually have to repeat. 

Here, the BounceBase is the base for any other bounce types. 
It sets up the collision, but leaves it for the interface to actually specify the behaviour. 
You have to remember to tie the behaviour back through the interface, or else this base class isn't going to run. 
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This is the base bouncing class that all the other types of bounces are going to use.
/// </summary>
public class BounceBase : MonoBehaviour
{
   public IBounce bounceType; //Make sure the interface is initiated so that the function can go through it 

   //We use protected so that the interface can change it as part of its behaviour 
   protected float speed;   
   protected Vector3 direction; 
   
   private ContactPoint2D[] myContact = new ContactPoint2D[1]; 
   
   /// <summary>
   /// Sets up the collider event for any inherited class. 
   /// </summary>
   /// <param name="collider"></param>
    protected void OnCollisionEnter2D(Collision2D collider)
    {
        TryBounce(); //On collision, we try to call the function coming through the interface. 
        
        collider.GetContacts(myContact);
        direction = Vector2.Reflect(direction, myContact[0].normal);
    }

   /// <summary>
   /// Actually moves the ball based on the speed and direction set by the interface. 
   /// </summary>
    void Update() 
    {
        this.transform.position += new Vector3(direction.x, direction.y, 0) * speed;
    }

    /// <summary>
    /// Attempts to call the behaviour that we've routed through the interface 
    /// </summary>
    void TryBounce()
    {
        bounceType?.Bounce();
    }
    
    
}