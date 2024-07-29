using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [HideInInspector] public float GunAngle { get; private set; }
    private PlayerController _player;
    private SpriteRenderer _gun;

    private void Awake()
    {
        InputManager.Instance.ActionsData.PlayerAimEvent.AddListener(Aim);
        _gun = GetComponentInChildren<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        transform.rotation = Quaternion.Euler(0,0,GunAngle);

        if (Mathf.Abs(GunAngle) <= 90)
        {
            _gun.flipY = false;
            _player.FlipPlayer(false);
        }
        else
        {
            _gun.flipY = true;
            _player.FlipPlayer(true);
        }
            
    }

}
