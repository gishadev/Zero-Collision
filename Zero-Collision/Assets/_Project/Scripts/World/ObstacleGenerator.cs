using Gisha.Optimisation;
using UnityEngine;

namespace Gisha.ZeroCollision.World
{
    public class ObstacleGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject obstaclePrefab = default;

        private void Start()
        {
            SpawnObstacle();
        }

        void SpawnObstacle()
        {
            PoolManager.Instantiate(obstaclePrefab, Vector2.zero, Quaternion.identity);
        }
    }
}