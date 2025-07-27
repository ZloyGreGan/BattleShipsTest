using UnityEngine;

namespace Game.Scripts.Pojectiles
{
    public class ProjectileLogic
    {
        public Vector3 MoveTowards(Vector3 currentPosition, Vector3 targetPosition, float speed, float deltaTime)
        {
            return Vector3.MoveTowards(currentPosition, targetPosition, speed * deltaTime);
        }

        public bool IsTargetReached(Vector3 currentPosition, Vector3 targetPosition, float hitDistance)
        {
            return Vector3.Distance(currentPosition, targetPosition) < hitDistance;
        }
    }
}