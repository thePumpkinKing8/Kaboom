using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : MonoBehaviour
{
    private bool _isShooting;
    private LineRenderer _laser;
    private PlayerController _player;
    private PlayerSettings _settings;
    private Vector2 _gunAngle;
    [SerializeField] private Transform _barrel;
    private RaycastHit2D _ray;
    private void Awake()
    {
        InputManager.Instance.ActionsData.PlayerShootEvent.AddListener(StartShooting);
        InputManager.Instance.ActionsData.PlayerShootCancel.AddListener(StopShooting);
        InputManager.Instance.ActionsData.PlayerAimEvent.AddListener(SetAngle);
        _barrel = transform.parent;
        _laser = GetComponent<LineRenderer>();
        _player = GetComponentInParent<PlayerController>();
        _settings = _player.Settings;
        _laser.enabled = false;
    }

    private void Update()
    {
        if(_isShooting)
        {
            Shoot();
            AddForce();
        }
    }

    private void StartShooting()
    {
        _isShooting = true;
        _laser.enabled = true;
    }

    private void StopShooting()
    {
        _isShooting = false;
        _laser.enabled = false;
        _player.XMomentum += _player.Rb.velocity.x;
    }

    private void Shoot()
    {
        _laser.SetPosition(0, _barrel.position);
        _ray = Physics2D.Raycast(_barrel.position, _gunAngle,100f, ~LayerMask.NameToLayer("Player"));
        
        if (_ray.collider != null)
        {
            _laser.SetPosition(1, _ray.point);
            Debug.DrawLine(_barrel.position, _ray.point);
        }
            
        else
        {
            _laser.SetPosition(1, (Vector2)_barrel.position + (_gunAngle * 100f));
            Debug.DrawLine(_barrel.position, (Vector2)_barrel.position + (_gunAngle * 100f));
        }
            
    }

    private void SetAngle(Vector2 pos, bool isMouse)
    {
        if (isMouse)
        {
            pos = Camera.main.ScreenToWorldPoint(pos);
            Vector2 direction = pos - new Vector2(transform.position.x, transform.position.y);
            float angle = Mathf.Atan2(direction.y, direction.x) ;
            _gunAngle = (new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)));
        }
        else
        {
            float angle = Mathf.Atan2(pos.y, pos.x);
            _gunAngle = (new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)));
        }
    }

    private void AddForce()
    {
        float shootingForce = 0;
        float distance = _ray.point.magnitude - _player.transform.position.magnitude;

        float _dist = Vector2.Distance(_player.transform.position, _ray.point);
        if (_dist != 0)
        {
            _dist = 1f / _dist;
            shootingForce = Mathf.Clamp((_dist / 100) * _settings.maxShootingForce, _settings.minShootingForce, _settings.maxShootingForce);
        }

        else
            shootingForce = _settings.maxShootingForce;

        Vector2 force = new Vector2(-shootingForce * _gunAngle.x, -shootingForce * _gunAngle.y);
        _player.AddForce(force);
    }

    
}
