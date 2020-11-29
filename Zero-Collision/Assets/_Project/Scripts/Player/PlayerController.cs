using Gisha.ZeroCollision.Game;
using UnityEngine;

namespace Gisha.ZeroCollision.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float horizontalSpeed = default;
        [SerializeField] private float rotationSpeed = default;
        [Space]
        [SerializeField] private float maxAngle = default;
        [SerializeField] private float minAngle = default;

        float _zAngle;

        Transform _transform;
        Camera _cam;
        GameManager _gameManager;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _cam = Camera.main;
            _transform = transform;
        }

        private void Update()
        {
            HorizontalMovement();

            if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
                RotateAnticlockwise();
            else
                RotateClockwise();

            ApplyRotation();
        }

        private void LateUpdate()
        {
            if (IsOutOfBounds(_transform.position)) _gameManager.Lose();
        }

        #region Movement
        void HorizontalMovement()
        {
            _transform.Translate(Vector2.right * horizontalSpeed * Time.deltaTime);
        }

        void RotateAnticlockwise()
        {
            _zAngle += rotationSpeed * Time.deltaTime;
            _zAngle = Mathf.Clamp(_zAngle, minAngle, maxAngle);
        }

        void RotateClockwise()
        {
            _zAngle -= rotationSpeed * Time.deltaTime;
            _zAngle = Mathf.Clamp(_zAngle, minAngle, maxAngle);
        }

        void ApplyRotation() => _transform.rotation = Quaternion.AngleAxis(_zAngle, Vector3.forward);
        #endregion

        #region Lose/Score

        bool IsOutOfBounds(Vector3 position) => Mathf.Abs(position.y) > _cam.orthographicSize;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Obstacle"))
                _gameManager.Lose();
        }

        #endregion
    }
}