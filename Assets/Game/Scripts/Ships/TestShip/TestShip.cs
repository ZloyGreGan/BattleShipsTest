using Game.Scripts.Entity;
using Game.Scripts.Ships.Components;
using UnityEngine;

namespace Game.Scripts.Ships
{
    public class TestShip : AEntity
    {
        [SerializeField] private MovementSettingsDTO _movementSettings;
        
        private ShipMovementComponent _shipMovementComponent;
        
        protected override void OnInitialize()
        {
            _shipMovementComponent = AddComponent<ShipMovementComponent>();
            _shipMovementComponent.SetMovementSettings(_movementSettings);
        }
    }
}