using System;
using Game.Scripts.Pojectiles;
using UnityEngine;

public class ShipProjectile : MonoBehaviour
{
    [SerializeField] private float _speed = 20f;
    [SerializeField] private GameObject _explosionEffect;

    private Transform _target;
    private readonly ProjectileLogic _projectileLogic = new();
    
    public event Action<Transform> OnTargetHit;

    private void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = _projectileLogic.MoveTowards(transform.position, _target.position, _speed, Time.deltaTime);

        if (_projectileLogic.IsTargetReached(transform.position, _target.position, 0.5f))
        {
            if (_explosionEffect != null)
            {
                Instantiate(_explosionEffect, _target.position, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning($"[{nameof(ShipProjectile)}] Explosion effect prefab is not assigned.");
            }

            OnTargetHit?.Invoke(_target);
            Destroy(gameObject);
            Destroy(_target.gameObject);
        }
    }
    
    public void SetTarget(Transform targetTransform) {
        _target = targetTransform;
    }

}