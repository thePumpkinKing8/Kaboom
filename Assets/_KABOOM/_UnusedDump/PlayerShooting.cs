using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class PlayerShooting : MonoBehaviour
{
    private Rigidbody2D _rb;
    private InputController _input;
    [SerializeField] private PlayerSettings _setting;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _input = GetComponent<InputController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_input.IsShoot)
            Shoot();
    }

    private void Shoot()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 _direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(_direction.y, _direction.x);

        Vector2 direction = (new Vector2(-(_setting.shootingForce * Mathf.Cos(angle)), -(_setting.shootingForce * Mathf.Sin(angle))));
        _rb.AddForce(direction);
    }
}
*/