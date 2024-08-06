using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlaceholder : MonoBehaviour
{
    private float _bulletLifetime = 5;

    private void Awake()
    {
        Destroy(gameObject, _bulletLifetime);
    }
}
