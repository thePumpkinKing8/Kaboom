using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : MonoBehaviour
{
    private LineRenderer _laser;
    private Vector2 _gunAngle;
    private GunController _gunController;
    private Transform _barrel;
    private RaycastHit2D _ray;
    [SerializeField] private float _laserWidth = .25f;
    private ParticleSystem _laserContactEffect;
    private void Awake()
    {
        InputManager.Instance.ActionsData.PlayerShootEvent.AddListener(StartShooting);
        InputManager.Instance.ActionsData.PlayerShootCancel.AddListener(StopShooting);
        LevelManager.Instance.EventData.NewLevelStartEvent.AddListener(LevelEnd);
        _barrel = transform.parent;
        _laser = GetComponent<LineRenderer>();
        _gunController = GetComponentInParent<GunController>();
       
        //_settings = _player.Settings;
        _laser.enabled = false;
    }

    private void LevelEnd(string tr)
    {
        _laserContactEffect.Stop();
    }



    private void StartShooting()
    {
        _laser.enabled = true;
        _laserContactEffect = PoolManager.Instance.Spawn("LaserContactEffect").GetComponent<ParticleSystem>();
        AudioManager.Instance.PlayAudio("Laser");
    }

    private void StopShooting()
    {
        AudioManager.Instance.StopAudio("Laser");
        _laser.enabled = false;
        _laserContactEffect.Stop();
        
    }

    private void Update()
    {
        if(_laser.enabled)
        {
            _gunAngle = (new Vector2(Mathf.Cos(_gunController.GunAngle * Mathf.Deg2Rad), Mathf.Sin(_gunController.GunAngle * Mathf.Deg2Rad))); //gets the angle the gun is facing
        }
    }

    /// <summary>
    /// sets the lasers endpoint position
    /// </summary>
    public void Shoot()
    {
        _laser.SetPosition(0, _barrel.position);
        _ray = Physics2D.CircleCast(_barrel.position,_laserWidth,_gunAngle,100f, ~LayerMask.GetMask("Player"));
        
        if (_ray.collider != null)
        {
            _laser.SetPosition(1, _ray.point);
            Debug.DrawLine(_barrel.position, _ray.point);
            CheckHit();
          //  Debug.Log(_ray.collider.name);
        }
            
        else
        {
            _laser.SetPosition(1, (Vector2)_barrel.position + (_gunAngle * 100f));
            Debug.DrawLine(_barrel.position, (Vector2)_barrel.position + (_gunAngle * 100f));
        }
        //sets the contact effects position to where the laser is hitting
        _laserContactEffect.transform.position = _ray.point;
            
    }


    /// <summary>
    /// returns a force vector based on the angle of the laser and the inputed force
    /// </summary>
    /// <param name="recoilForce"></param>
    /// <returns></returns>
    public Vector2 ForceVector(float recoilForce)
    {
        /* code for chnaging force based on distance from object hit by the laser. commented out incase we want to go back to this
         
        float distance = _ray.point.magnitude - _player.transform.position.magnitude;

        float _dist = Vector2.Distance(_player.transform.position, _ray.point);
        Debug.Log(_dist);
        if (_dist != 0)
        {
            _dist = 1f / _dist;
            shootingForce = Mathf.Clamp((_dist / 100) * _settings.maxShootingForce, _settings.minShootingForce, _settings.maxShootingForce);
        }

        else
           */ 

        Vector2 force = new Vector2(-recoilForce * _gunAngle.x, -recoilForce * _gunAngle.y);
        //_player.AddForce(force);
        return force;
    }

    private void CheckHit()
    {
        if (_ray.collider.GetComponent<BreakableTile>() != null)
        {
            _ray.collider.GetComponent<BreakableTile>().BreakTile(_ray.point);
        }
        if(_ray.collider.GetComponent<IDestructable>() != null)
        {
            _ray.collider.GetComponent<IDestructable>().ObjectDestroyed();
        }
    }

    
}
