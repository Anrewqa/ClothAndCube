using UnityEngine;

namespace GameLogic
{
    public class MeshGenerator : MonoBehaviour
    {
        [SerializeField] private ClothObject _leftCloth;
        [SerializeField] private ClothObject _rightCloth;
        [SerializeField] private Material _material;

        public void UpdateMeshes(Vector3[] leftPoints, Vector3[] rightPoints, int subdivisionCount)
        {
            _leftCloth.SkinnedMeshRenderer.sharedMesh = GenerateMesh(leftPoints, subdivisionCount, false);
            _leftCloth.SkinnedMeshRenderer.sharedMaterial = _material;
            
            _rightCloth.SkinnedMeshRenderer.sharedMesh = GenerateMesh(rightPoints, subdivisionCount, true);
            _rightCloth.SkinnedMeshRenderer.sharedMaterial = _material;
        }

        private Mesh GenerateMesh(Vector3[] points, int subdivisionLevel, bool isNormalInverted)
        {
            var mesh = new Mesh();

            var segmentLength = 2 + subdivisionLevel;
            var segmentsCount = points.Length / segmentLength;
            
            var uv = new Vector2[points.Length];

            for (int y = 0; y < segmentsCount; y++)
            {
                var firstPoint = points[y * segmentLength];
                var lastPoint = points[y * segmentLength + segmentLength - 1];
                
                for (int x = 0; x < segmentLength; x++)
                {
                    var point = points[x + y * segmentLength];
                    var uvX = Vector3.Distance(point, firstPoint) / Vector3.Distance(lastPoint, firstPoint);
                    
                    uv[y * segmentLength + x] = new Vector2(uvX, (float) y / segmentsCount);
                }
            }

            segmentLength--;
            segmentsCount--;

            var triangles = new int[segmentsCount * segmentLength * 6];
            for (int ti = 0, vi = 0, y = 0; y <= segmentsCount - 1; y++, vi++)
            {
                for (int x = 0; x <= segmentLength - 1; x++, ti += 6, vi++)
                {
                    triangles[ti] = vi;
                    if (isNormalInverted)
                    {
                        triangles[ti + 3] = triangles[ti + 2] = vi + segmentLength + 1;
                        triangles[ti + 4] = triangles[ti + 1] = vi + 1;
                    }
                    else
                    {
                        triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                        triangles[ti + 4] = triangles[ti + 1] = vi + segmentLength + 1;
                    }
                    triangles[ti + 5] = vi + segmentLength + 2;
                }
            }

            mesh.vertices = points;
            mesh.uv = uv;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();

            return mesh;
        }
    }
}