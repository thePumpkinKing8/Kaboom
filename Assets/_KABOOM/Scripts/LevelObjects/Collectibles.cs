using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collect();
    }

    protected virtual void Collect()
    {
        Destroy(this.gameObject);
    }
}
