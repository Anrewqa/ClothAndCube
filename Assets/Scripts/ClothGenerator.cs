using System;
using Settings;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameLogic
{
    [ExecuteAlways]
    public class ClothGenerator : MonoBehaviour
    {
        [SerializeField] private ClothSettings _clothSettings;
        [SerializeField] private MeshGenerator _meshGenerator;
        [SerializeField] private int _subdivision;
        [SerializeField] private bool _isGenerate;
        
        [SerializeField, HideInInspector] private Vector3[] _midPoints;
        [SerializeField, HideInInspector]private Vector3[] _leftPoints;
        [SerializeField, HideInInspector]private Vector3[] _rightPoints;

        public Vector3[] Path => _midPoints;

        private void Update()
        {
            if (_isGenerate)
            {
                Generate();

                _meshGenerator.UpdateMeshes(_leftPoints, _rightPoints, _subdivision);
                
                _isGenerate = false;
            }
        }

        private void Generate()
        {
            _leftPoints = new Vector3[(_clothSettings.PathControlPointsCount + 2) * (2 + _subdivision)];
            _rightPoints = new Vector3[(_clothSettings.PathControlPointsCount + 2) * (2 + _subdivision)];
            _midPoints = new Vector3[_clothSettings.PathControlPointsCount + 2];

            var pointsCount = _clothSettings.PathControlPointsCount + 2;
            
            for (int i = 0; i < pointsCount; i++)
            {
                if (i == 0)
                {
                    var startPoint = transform.position;
                    _midPoints[0] = startPoint;
                    SaveSection(startPoint, 0);
                    continue;
                }
                
                if (i == pointsCount - 1)
                {
                    var endPoint = transform.position + Vector3.forward * _clothSettings.Length;
                    SaveSection(endPoint, i);
                    _midPoints[i] = endPoint;
                    continue;
                }

                var midPoint = GetRandomMidPoint(i, pointsCount);

                _midPoints[i] = midPoint;
                SaveSection(midPoint, i);
            }
        }

        private Vector3 GetRandomMidPoint(int index, int pointsCount)
        {
            var distance = _clothSettings.Length * (index + .5f) / pointsCount;
            var midPoint = transform.position + Vector3.forward * distance;
            var offsetRandomness = _clothSettings.PathSectionVerticalOffsetRandomness;
            midPoint += Vector3.forward * Random.Range(-offsetRandomness, offsetRandomness);
            offsetRandomness = _clothSettings.PathSectionHorizontalOffsetRandomness;
            midPoint += Vector3.left * Random.Range(-offsetRandomness, offsetRandomness);

            return midPoint;
        }

        private void SaveSection(Vector3 midPoint, int sectionIndex)
        {
            var leftEdgePoint = new Vector3(-_clothSettings.Width, midPoint.y, midPoint.z);
            var leftMidPoint = new Vector3(midPoint.x - _clothSettings.PathWidth, midPoint.y, midPoint.z);
            SaveSectionForSide(_leftPoints, leftEdgePoint, leftMidPoint, sectionIndex);
            
            var rightEdgePoint = new Vector3(_clothSettings.Width, midPoint.y, midPoint.z);
            var rightMidPoint = new Vector3(midPoint.x + _clothSettings.PathWidth, midPoint.y, midPoint.z);
            SaveSectionForSide(_rightPoints, rightEdgePoint, rightMidPoint, sectionIndex);
        }

        private void SaveSectionForSide(Vector3[] sidePoints, Vector3 sidePoint, Vector3 midPoint, int sectionIndex)
        {
            var pointsInSection = 2 + _subdivision;
            
            for (int i = 0; i < pointsInSection; i++)
            {
                var pointIndex = pointsInSection * sectionIndex + i;
                
                if (i == 0)
                {
                    sidePoints[pointIndex] = sidePoint;
                    continue;
                }

                if (i == pointsInSection - 1)
                {
                    sidePoints[pointIndex] = midPoint;
                    continue;
                }
                
                sidePoints[pointIndex] = sidePoint + (midPoint - sidePoint) / (_subdivision + 1) * i;
            }
        }
        

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            foreach (var leftPoint in _leftPoints)
            {
                Gizmos.DrawSphere(leftPoint, .5f);
            }
            
            Gizmos.color = Color.white;
            foreach (var midPoint in _midPoints)
            {
                Gizmos.DrawSphere(midPoint, .5f);
            }
            
            Gizmos.color = Color.blue;
            foreach (var rightPoint in _rightPoints)
            {
                Gizmos.DrawSphere(rightPoint, .5f);
            }
        }
    }
}