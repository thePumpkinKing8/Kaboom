using UnityEngine;
using UnityEngine.Events;

namespace _KABOOM.Scripts.PlayerScripts
{
    [CreateAssetMenu(fileName = "PlayerActionsData", menuName = "SOs/PlayerActionsData", order = 0)]
    public class PlayerActionsData : ScriptableObject
    {
        public UnityEvent<Vector2> PlayerMovementEvent;
        public UnityEvent PlayerJumps;
        public void HandlePlayerMovement(Vector2 movement) => PlayerMovementEvent?.Invoke(movement);
        public void HandlePlayerJumps() => PlayerJumps?.Invoke();
    }
}