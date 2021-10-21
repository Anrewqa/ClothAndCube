using GameLogic.CustomInput;
using UnityEngine;

namespace GameLogic
{
    public class SwipableObject : MonoBehaviour
    {
        [SerializeField] private SwipeController _swipeController;

        private Rigidbody _rigidbody;
        
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            
            _swipeController.Swipe += OnSwipe;
        }

        private void OnSwipe(SwipeDirections direction)
        {
            transform.localPosition += direction == SwipeDirections.Left ? Vector3.left : Vector3.right;
        }
    }
}