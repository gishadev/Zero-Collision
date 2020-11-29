using System.Collections.Generic;
using UnityEngine;

namespace Gisha.ZeroCollision.World
{
    public class ParallaxEffect : MonoBehaviour
    {
        [Header("Effect")]
        [SerializeField] private float parallaxSpeed = default;

        [Header("Spawning")]
        [SerializeField] private int copiesCount = default;
        [SerializeField] private GameObject bgPrefab = default;
        Sprite BGSprite => bgPrefab.GetComponent<SpriteRenderer>().sprite;

        List<Transform> _bgs = new List<Transform>();
        int _replacingIndex = 0;

        Transform _transform;
        Camera _cam;

        private void Awake()
        {
            _transform = transform;
            _cam = Camera.main;
        }

        private void Start()
        {
            InstantiateBackgrounds();
        }

        private void Update()
        {
            MoveHorizontally();

            if (IsOutOfView())
            {
                ReplaceBackground();

                _replacingIndex++;
                if (_replacingIndex + 1 > copiesCount)
                    _replacingIndex = 0;

            }
        }

        void MoveHorizontally()
        {
            Vector3 newPos = new Vector3(_transform.localPosition.x - Time.deltaTime * parallaxSpeed, 0f, 10f);
            _transform.localPosition = newPos;
        }

        void InstantiateBackgrounds()
        {
            float width = BGSprite.bounds.size.x;

            for (int i = 0; i < copiesCount; i++)
            {
                float xPos = (width * i) - width;
                Vector3 position = Vector3.right * xPos;
                _bgs.Add(Instantiate(bgPrefab, position, Quaternion.identity, _transform).transform);
            }
        }

        void ReplaceBackground()
        {
            float width = BGSprite.bounds.size.x;
            _bgs[_replacingIndex].position += Vector3.right * (copiesCount * width);
        }

        bool IsOutOfView()
        {
            float replacingBGMaxX = _bgs[_replacingIndex].position.x + BGSprite.bounds.size.x / 2f;

            float camCenterX = _cam.transform.position.x;

            return (replacingBGMaxX - (camCenterX - 25f)) < 0;
        }
    }
}