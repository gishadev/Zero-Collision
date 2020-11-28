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


        Transform _transform;
        float _zAngle;

        private void Awake()
        {
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
    }
}