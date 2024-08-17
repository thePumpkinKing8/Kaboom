using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [HideInInspector] public float GunAngle { get; private set; }
    private SpriteRenderer _gun;
    private SpriteRenderer _playerSprite;
    [SerializeField] private SpriteRenderer _armSprite;

    private void Awake()
    {
        InputManager.Instance.ActionsData.PlayerAimEvent.AddListener(Aim);
        _gun = GetComponentInChildren<SpriteRenderer>();
        _playerSprite = GetComponentInParent<SpriteRenderer>();
    }
   

    private void Aim(Vector2 pos, bool isMouse)
    {
        if(isMouse)
        {
            pos = Camera.main.ScreenToWorldPoint(pos);
            Vector2 direction = pos - new Vector2(transform.position.x, transform.position.y);
           
            GunAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        }
        else
        {
            GunAngle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
        }
        

        if (Mathf.Abs(GunAngle) <= 90)
        {
           // _gun.flipY = false;
           // _armSprite.flipX = false;
            //_armSprite.flipY = false;
            //_playerSprite.flipX = false;
            _playerSprite.transform.localScale = new Vector3(1, 1, 1);
            transform.rotation = Quaternion.Euler(0, 0, GunAngle);
        }
        else
        {
            //_gun.flipY = true;
            //_armSprite.flipX = true;
            //_armSprite.flipY = false;
            // _playerSprite.flipX = true;
            _playerSprite.transform.localScale = new Vector3(-1, 1, 1);
            transform.rotation = Quaternion.Euler(0, 0, GunAngle-180);
        }
            
    }

}
