using Game.Scripts.Entity;
using Game.Scripts.Entity.Components;
using UnityEngine;

namespace Game.Scripts.Ships.Components
{
    public class ShipMovementComponent : IEntityComponent, IStarterComponent, IUpdatableComponent
    {
        private MovementSettingsDTO _movementSettings;
        private AEntity _entity;
        private Vector3 _targetPoint;
        private MovementLogic _movementLogic = new();
        private Vector3 _flatTarget;

        public void Initialize()
        {
            _flatTarget = _entity.Position;
        }

        public void SetOwner(AEntity entity)
        {
            _entity = entity ?? throw new System.ArgumentNullException(nameof(entity));
        }
        
        public void Start()
        {
            PickNewTarget();
        }

        public void Update()
        {
            _flatTarget.y = _entity.Position.y;
            Vector3 direction = _movementLogic.CalculateDirection(_entity.Position, _flatTarget);
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = _movementLogic.CalculateRotation(direction);
                _entity.Rotation = _movementLogic.SmoothRotate(
                    _entity.Rotation, 
                    targetRotation, 
                    _movementSettings.RotationSpeed, 
                    Time.deltaTime
                );
                _entity.Position += _movementLogic.CalculateMovement(
                    _entity.Forward, 
                    _movementSettings.MoveSpeed, 
                    Time.deltaTime
                );
            }

            if (_movementLogic.IsTargetReached(_entity.Position, _flatTarget, _movementSettings.TargetRadius))
            {
                PickNewTarget();
            }
        }
        
        public void SetMovementSettings(MovementSettingsDTO movementSettings)
        {
            _movementSettings = movementSettings ?? throw new System.ArgumentNullException(nameof(movementSettings));
        }
        
        private void PickNewTarget()
        {
            _flatTarget = _movementLogic.GenerateRandomTarget(_entity.Position.y, _movementSettings.TargetRange);
        }
    }
}