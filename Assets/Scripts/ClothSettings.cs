using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "ClothSettings", menuName = "Settings/Cloth", order = 0)]
    public class ClothSettings : ScriptableObject
    {
        [SerializeField] private float _length;
        [SerializeField] private float _width;
        [SerializeField] private float _pathWidth;
        [SerializeField] private int _pathControlPointsCount;
        [SerializeField] private float _pathSectionVerticalOffsetRandomness;
        [SerializeField] private float _pathSectionHorizontalOffsetRandomness;

        public float Length => _length;
        public float Width => _width;
        public float PathWidth => _pathWidth;
        public int PathControlPointsCount => _pathControlPointsCount;
        public float PathSectionVerticalOffsetRandomness => _pathSectionVerticalOffsetRandomness;
        public float PathSectionHorizontalOffsetRandomness => _pathSectionHorizontalOffsetRandomness;
    }
}