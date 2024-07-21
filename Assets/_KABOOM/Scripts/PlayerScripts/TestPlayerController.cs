using System;
using UnityEngine;

namespace _KABOOM.Scripts.PlayerScripts
{
    public class TestPlayerController : MonoBehaviour, IPlayerControls
    { 
        [SerializeField] private Transform groundCheck;

        [SerializeField] private Transform aimDirection;
        private float _lastDirection;
        private float _gunAngle = 0;
        private Vector2 _gunDirection;
        private Vector2 _gunForce;
        private bool _isShooting = false;
        [HideInInspector] public float LastDirection 
        { 
            get
            {
                return _lastDirection;
            }
        
            set 
            {
                _lastDirection = (value > 0 ? 1 : -1); 
            } 
        }
        private float _horizontal;

        private float _currentHealth;
        public float Horizontal 
        {
            get
            {
                return _horizontal;
            }

            //this is messy and may no longer be needed. clean up after prototype
            private set
            {
                if (value != 0)
                {
                    _horizontal = value;
                    if(LastDirection != value)
                    {
                        LastDirection = value;
                        FlipPlayer();
                    }
                }
                else
                    _horizontal = value;
            } 
        }
        [SerializeField] PlayerSettings Settings;
        [SerializeField] protected PlayerActionsData playerActionsData;
        private Rigidbody2D _rb;
        private float _xMomentum;
        private InputController inputController;
        private Animator anim;
        
        public bool IsGrounded() => Physics2D.OverlapCircle(groundCheck.position, Settings.groundCheckRadius, Settings.groundLayerMask);
        private void Awake()
        {
            //if (GameManager.Instance.player == null)
            //  GameManager.Instance.player = this;

            _rb = GetComponent<Rigidbody2D>();
            inputController = GetComponent<InputController>();
            anim = GetComponent<Animator>();

            //set player health
            _currentHealth = Settings.maxHealth;

            LastDirection = 1;

            _rb.gravityScale = Settings.gravityScale;
        }
        private void FlipPlayer()
        {
            var size = transform.localScale;
            Vector3 direction = GetPlayerDirection();

           // transform.localScale = new Vector3(direction.x * Mathf.Abs(size.x),size.y,size.z); 
        }
        private Vector3 GetPlayerDirection()
        {
            return Horizontal switch
            {
                > 0 => Vector3.right,
                < 0 => Vector3.left,
                _ => new Vector3(Mathf.Round(LastDirection),0,0),
            };
        }
        enum PlayerDirection{Left, Right}

        public void HandleMovement(Vector2 movement)
        {
            Horizontal = movement.x;
            Debug.Log($"Movement value is {movement.x}");
        }

        public void HandleDirection(Vector2 direction)
        {
            if (direction == Vector2.zero)
                return;
            _gunDirection = direction;
            if (_isShooting)
            {
                //var gunMagnitude = _gunForce.magnitude;
                _gunForce = -_gunDirection.normalized;
               // _gunForce *= gunMagnitude;

            }
                
            Debug.Log($"Direction value is {direction}");
            _gunAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            aimDirection.transform.rotation = Quaternion.Euler(0, 0, _gunAngle);
        }

        public void HandleJump()
        {
            if (!IsGrounded()) return;

            //player.jumpEvent.Raise();
            _rb.velocity = new Vector2(_rb.velocity.x, Settings.jumpHeight);
            playerActionsData.HandlePlayerJumps();
        }

        [SerializeField] private float maxGunMagnitude = 10;
        [SerializeField] private float gunAccelerationSpeed = 2;
        private void FixedUpdate()
        {
            
            if(_gunForce.magnitude < maxGunMagnitude && _isShooting)
                _gunForce += -_gunDirection * Time.fixedDeltaTime * gunAccelerationSpeed;
            else if (_isShooting && _gunForce.magnitude > maxGunMagnitude)
                _gunForce = Vector2.zero;
            
            _rb.velocity = new Vector2(Horizontal * Settings.movementSpeed + _xMomentum, _rb.velocity.y) + _gunForce;
            playerActionsData.HandlePlayerMovement(_rb.velocity);

        }

        protected void HandleMomentum()
        {
            float sign = Mathf.Sign(_xMomentum);
            _xMomentum = (Mathf.Abs(_xMomentum) - Settings.playerFriction);
            if (_xMomentum<= 0)
                _xMomentum = 0;
            else
                _xMomentum *= sign;
        }
        public void HandleShoot()
        {
            _gunForce = -_gunDirection; 
            _isShooting = true;
        }

        public void StopShooting()
        {
            _isShooting = false;
            _gunForce = Vector2.zero;
        }
    }
}