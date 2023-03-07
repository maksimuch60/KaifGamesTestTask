using UnityEngine;
using Random = UnityEngine.Random;

namespace Cube
{
    [RequireComponent(typeof(Rigidbody))]
    public class CubeMovement : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _rotationSpeed;

        private Rigidbody _rb;
        private Transform _upperBound;
        private Transform _lowerBound;
        private Transform _cachedTransform;

        private bool _isPointValid;
        private Vector3 _currentPoint;

        public void Construct(Transform upperBound, Transform lowerBound)
        {
            _upperBound = upperBound;
            _lowerBound = lowerBound;
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _cachedTransform = transform;
        }

        private void Update()
        {
            if (_isPointValid)
            {
                MoveToPoint();
                RotateToPoint();
                IsPointValid();
            }
            else
            {
                GetRandomPoint();
            }
        }

        private void IsPointValid()
        {
            if ((_cachedTransform.position - _currentPoint).magnitude < 0.3f)
            {
                _isPointValid = false;
            }
        }

        private void MoveToPoint()
        {
            Vector3 direction = (_currentPoint - _cachedTransform.position).normalized;
            SetVelocity(direction * _movementSpeed);
        }

        private void SetVelocity(Vector3 velocity)
        {
            _rb.velocity = velocity;
        }

        private void RotateToPoint()
        {
            Vector3 direction = _currentPoint - _cachedTransform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            _cachedTransform.rotation = 
                Quaternion.Lerp(_cachedTransform.rotation, rotation, _rotationSpeed * Time.deltaTime);
        }
        
        private void GetRandomPoint()
        {
            Vector3 lowerPosition = _lowerBound.position;
            Vector3 upperPosition = _upperBound.position;
            float x = Random.Range(lowerPosition.x, upperPosition.x);
            float z = Random.Range(lowerPosition.z, upperPosition.z);

            _currentPoint = new Vector3(x, 0.5f, z);

            _isPointValid = true;
        }
    }
}
