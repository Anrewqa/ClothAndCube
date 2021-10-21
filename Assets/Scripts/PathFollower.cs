using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace GameLogic
{
    public class PathFollower : MonoBehaviour
    {
        public event Action PathComplete;
        
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _stoppingDistance;

        private bool _isInitialized;
        private int _index;
        private Vector3[] _path;

        public void Initialize(Vector3[] path)
        {
            _path = path;
            _isInitialized = true;
        }

        private void Update()
        {
            if (!_isInitialized)
            {
                return;
            }

            var moveSpeed = _moveSpeed * Time.deltaTime;
            var rotationSpeed = _rotationSpeed * Time.deltaTime;

            var currentPosition = transform.position;
            currentPosition = Vector3.MoveTowards(currentPosition, _path[_index], moveSpeed);
            transform.position = currentPosition;

            var direction = _path[_index] - currentPosition;
            var desiredRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed);

            if (Vector3.Distance(_path[_index], transform.position) < _stoppingDistance)
            {
                if (++_index >= _path.Length)
                {
                    PathComplete?.Invoke();
                    _isInitialized = false;
                }
            }
        }
    }
}