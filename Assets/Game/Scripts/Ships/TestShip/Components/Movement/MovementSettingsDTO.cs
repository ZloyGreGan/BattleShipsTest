using System;
using UnityEngine;

namespace Game.Scripts.Ships.Components
{
    [Serializable]
    public class MovementSettingsDTO
    {
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _rotationSpeed = 0.1f;
        [SerializeField] private float _targetRadius = 1f;
        [SerializeField] private float _targetRange = 50f;

        public float MoveSpeed => _moveSpeed;
        public float RotationSpeed => _rotationSpeed;
        public float TargetRadius => _targetRadius;
        public float TargetRange => _targetRange;
    }
}