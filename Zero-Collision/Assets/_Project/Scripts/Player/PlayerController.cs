using Gisha.Effects.Audio;
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

        private void Awake()
        {
            _cam = Camera.main;
            _transform = transform;
        }

        private void Update()
        {
            HorizontalMovement();

            if (!GameManager.Instance.IsPlaying)
                return;

            if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
                RotateAnticlockwise();
            else
                RotateClockwise();

            ApplyRotation();
        }

        private void LateUpdate()
        {
            if (IsOutOfBounds(_transform.position)) Die();
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

        #region Loosing

        bool IsOutOfBounds(Vector3 position) => Mathf.Abs(position.y) > _cam.orthographicSize;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Obstacle"))
                Die();

            if (other.CompareTag("Score"))
            {
                GameManager.Instance.AddScore();
                AudioManager.Instance.PlaySFX("drop_004");
            }
        }

        void Die()
        {
            gameObject.SetActive(false);
            GameManager.Instance.Lose();

            AudioManager.Instance.PlaySFX("error_007");
        }

        #endregion
    }
}