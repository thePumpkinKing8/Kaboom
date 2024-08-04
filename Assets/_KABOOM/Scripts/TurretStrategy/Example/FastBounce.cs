using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FastBounce : MonoBehaviour
{
    float speed = 0.1f;
    private Vector3 direction; 
    

    ContactPoint2D[] myContact = new ContactPoint2D[1];

    void Start()
    {
        direction = new Vector3(0, -1, 0);

    }

    void OnCollisionEnter2D (Collision2D collider)
    {
        Debug.Log("collided");
        collider.GetContacts(myContact);
        direction = Vector2.Reflect(direction, myContact[0].normal);
    }


    public void Update ()
    {
        this.transform.position += new Vector3(direction.x, direction.y, 0) * speed;



    }
}
