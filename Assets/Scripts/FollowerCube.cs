using UnityEngine;

namespace GameLogic
{
    [RequireComponent(typeof(Rigidbody))]
    public class FollowerCube : MonoBehaviour
    {
        [SerializeField] private Transform _transformToFollow;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotationSpeed;

        private Rigidbody _rigidbody;
        
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            var position = Vector3.MoveTowards(transform.position, _transformToFollow.position,
                Time.deltaTime * _moveSpeed);
            var rotation = Quaternion.RotateTowards(transform.rotation, _transformToFollow.rotation,
                Time.deltaTime * _rotationSpeed);
            
            _rigidbody.MovePosition(position);
            _rigidbody.MoveRotation(rotation);
        }
    }
}