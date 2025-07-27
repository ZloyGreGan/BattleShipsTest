using UnityEngine;

namespace Game.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject _projectilePrefab;
        
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _targetShip;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            GameObject proj = Instantiate(_projectilePrefab, _spawnPoint.position, Quaternion.identity);
            if (proj.TryGetComponent<ShipProjectile>(out var projectile))
            {
                projectile.SetTarget(_targetShip);
            }
        }
    }
}