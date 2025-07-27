using UnityEngine;

namespace Game.Scripts.Ships.Components
{
    public class MovementLogic
    {
        public Vector3 CalculateDirection(Vector3 currentPosition, Vector3 targetPosition)
        {
            return (targetPosition - currentPosition).normalized;
        }

        public Quaternion CalculateRotation(Vector3 direction)
        {
            return Quaternion.LookRotation(direction);
        }

        public Quaternion SmoothRotate(Quaternion currentRotation, Quaternion targetRotation, float rotationSpeed, float deltaTime)
        {
            return Quaternion.RotateTowards(currentRotation, targetRotation, rotationSpeed * deltaTime * Mathf.Rad2Deg);
        }

        public Vector3 CalculateMovement(Vector3 forward, float moveSpeed, float deltaTime)
        {
            return forward * moveSpeed * deltaTime;
        }

        public bool IsTargetReached(Vector3 currentPosition, Vector3 targetPosition, float targetRadius)
        {
            return (currentPosition - targetPosition).sqrMagnitude <= targetRadius * targetRadius;
        }

        public Vector3 GenerateRandomTarget(float yPosition, float range)
        {
            return new Vector3(
                Random.Range(-range, range),
                yPosition,
                Random.Range(-range, range)
            );
        }
    }
}