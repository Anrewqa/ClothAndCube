using System;
using GameLogic.CustomInput;
using UnityEngine;

namespace GameLogic
{
    public class SwipableObject : MonoBehaviour
    {
        [SerializeField] private SwipeController _swipeController;
        [SerializeField] private float _maxOffset;

        private Rigidbody _rigidbody;
        
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            
            _swipeController.Swipe += OnSwipe;
        }

        private void OnDestroy()
        {
            _swipeController.Swipe -= OnSwipe;
        }

        private void OnSwipe(SwipeDirections direction)
        {
            var desiredPosition = transform.localPosition + (direction == SwipeDirections.Left ? Vector3.left : Vector3.right);
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, -_maxOffset, _maxOffset);
            transform.localPosition = desiredPosition;
        }
    }
}