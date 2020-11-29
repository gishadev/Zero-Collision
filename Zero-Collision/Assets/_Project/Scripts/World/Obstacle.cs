using System.Collections;
using UnityEngine;

namespace Gisha.ZeroCollision.World
{
    public class Obstacle : MonoBehaviour
    {
        Transform _transform;
        Transform _camTransform;

        private void Awake()
        {
            _transform = transform;
            _camTransform = Camera.main.transform;
        }

        IEnumerator DespawnChecking()
        {
            yield return new WaitUntil(() => IsReadyToBeDeactivated());
            gameObject.SetActive(false);
        }

        bool IsReadyToBeDeactivated() => (_camTransform.position - _transform.position).x > 15f;

        private void OnEnable()
        {
            StartCoroutine(DespawnChecking());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }
    }
}