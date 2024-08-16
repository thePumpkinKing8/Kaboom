using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            SetSpawnPoint();
        }
    }
    private void SetSpawnPoint()
    {
        Debug.Log("set");
        LevelManager.Instance.NewSpawnPoint(transform.position);
    }
}
