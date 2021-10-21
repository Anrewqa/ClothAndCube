using System;
using UnityEngine;

namespace GameLogic.CustomInput
{
    public class SwipeController : MonoBehaviour
    {
        private const int MaxPositionsSaved = 10;
        
        public event Action<SwipeDirections> Swipe;

        [SerializeField] private float _swipePositionDeltaThreshold;
        [SerializeField] private float _swipeMagnitudeDeltaThreshold;
        [SerializeField] private Transform _transform;

        private Vector3 _inputStartPosition;
        private Vector3[] _previousPositions;
        private int _positionIndex;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnInputStart();
            }

            if (Input.GetMouseButton(0))
            {
                OnInputUpdate();
            }

            if (Input.GetMouseButtonUp(0))
            {
                OnInputEnd();
            }
        }

        private void OnInputStart()
        {
            _inputStartPosition = Input.mousePosition;
            _previousPositions = new Vector3[MaxPositionsSaved];
            _positionIndex = 0;
        }

        private void OnInputUpdate()
        {
            _previousPositions[_positionIndex] = Input.mousePosition;
            _positionIndex = (_positionIndex + 1) % MaxPositionsSaved;
        }

        private void OnInputEnd()
        {
            var swipeVector = GetSwipeVector();
            var isPointerMoved = Vector3.Distance(_inputStartPosition, Input.mousePosition) >
                                 _swipePositionDeltaThreshold;
            var isPointerMovedEnough = swipeVector.magnitude > _swipeMagnitudeDeltaThreshold;
            if (isPointerMoved && isPointerMovedEnough)
            {
                var direction = Vector3.Dot(Input.mousePosition - _inputStartPosition, Vector2.left) >= 0
                    ? SwipeDirections.Left
                    : SwipeDirections.Right;
                
                Swipe?.Invoke(direction);
            }
        }

        private Vector3 GetSwipeVector()
        {
            Vector3 result = Vector3.zero;
            for (int i = 0; i < _previousPositions.Length - 1; i++)
            {
                var current = _previousPositions[i];
                var next = _previousPositions[i + 1];

                if (current == Vector3.zero || next == Vector3.down)
                {
                    continue;
                }

                result += next - current;
            }

            return result;
        }
    }
}
