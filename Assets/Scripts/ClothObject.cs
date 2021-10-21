using UnityEngine;

namespace GameLogic
{
    [RequireComponent(typeof(MeshFilter), typeof(SkinnedMeshRenderer), typeof(Cloth))]
    public class ClothObject : MonoBehaviour
    {
        [SerializeField] private MeshFilter _meshFilter;
        [SerializeField] private SkinnedMeshRenderer _meshRenderer;

        public MeshFilter MeshFilter
        {
            get
            {
                if (_meshFilter == null)
                {
                    _meshFilter = GetComponent<MeshFilter>();
                }

                return _meshFilter;
            }
        }

        public SkinnedMeshRenderer SkinnedMeshRenderer
        {
            get
            {
                if (_meshRenderer == null)
                {
                    _meshRenderer = GetComponent<SkinnedMeshRenderer>();
                }

                return _meshRenderer;
            }
        }
    }
}