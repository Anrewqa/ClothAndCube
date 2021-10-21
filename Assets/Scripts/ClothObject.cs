using UnityEngine;

namespace GameLogic
{
    [RequireComponent(typeof(SkinnedMeshRenderer), typeof(Cloth))]
    public class ClothObject : MonoBehaviour
    {
        [SerializeField] private SkinnedMeshRenderer _meshRenderer;

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