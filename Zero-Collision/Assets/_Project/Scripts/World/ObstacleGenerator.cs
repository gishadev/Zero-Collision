using Gisha.Optimisation;
using System.Collections.Generic;
using UnityEngine;

namespace Gisha.ZeroCollision.World
{
    public class ObstacleGenerator : MonoBehaviour
    {
        [Header("Prefab")]
        [SerializeField] private GameObject obstaclePrefab = default;

        [Header("Distance To Spawn")]
        [SerializeField] private float minDistToSpawn = default;
        [SerializeField] private float maxDistToSpawn = default;

        [Header("Heights")]
        [SerializeField] private float minHeight = default;
        [SerializeField] private float maxHeight = default;

        public Vector2 PositionOutOfCameraView
            => (Vector2)_cam.transform.position + Vector2.right * (_cam.orthographicSize * Screen.width / Screen.height + 1.25f);

        List<GameObject> _obstaclesList = new List<GameObject>();
        float _distToSpawn;

        Transform _lastSpawnedTrans;
        Camera _cam;

        private void Awake()
        {
            _cam = Camera.main;
        }

        private void LateUpdate()
        {
            if (IsReadyToSpawn())
            {
                Vector2 newPos = PositionOutOfCameraView + Vector2.up * GetRandomHeight();
                SpawnObstacle(newPos);
                _distToSpawn = Random.Range(minDistToSpawn, maxDistToSpawn);
            }
        }

        void SpawnObstacle(Vector2 newPosition)
        {
            var o = PoolManager.Instantiate(obstaclePrefab, newPosition, Quaternion.identity);

            if (!_obstaclesList.Contains(o))
                _obstaclesList.Add(o);

            _lastSpawnedTrans = o.transform;
        }

        float GetRandomHeight() => Random.Range(minHeight, maxHeight);
        bool IsReadyToSpawn()
            => _lastSpawnedTrans == null || (_lastSpawnedTrans.position - _cam.transform.position).x < _distToSpawn;

    }
}