using UnityEngine;

namespace _KABOOM.Scripts.PlayerScripts
{
    public interface IPlayerControls
    {
        void HandleMovement(Vector2 movement);
        void HandleDirection(Vector2 direction);
        void HandleJump();
        void HandleShoot();
        void StopShooting();
    }
}