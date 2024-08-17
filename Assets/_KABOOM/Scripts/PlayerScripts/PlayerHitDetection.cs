using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitDetection : PlayerHealth
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BulletPlaceholder>())
        {
            LevelManager.Instance.EventData.PlayerKilledEvent.Invoke("PlayerHit");
        }
    }
}
