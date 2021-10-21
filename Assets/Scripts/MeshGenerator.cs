using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    public class MeshGenerator : MonoBehaviour
    {
        [SerializeField] private Material _material;

        [SerializeField, HideInInspector] private List<GameObject> _spawnedMeshes = new List<GameObject>();

        public void ClearSpawnedMeshes()
        {
            if (_spawnedMeshes.Count > 0)
            {
                foreach (var spawnedMesh in _spawnedMeshes)
                {
                    DestroyImmediate(spawnedMesh);
                }
                
                _spawnedMeshes.Clear();
            }
        }
        
        
        public void GenerateMesh(Vector3[] points, int subdivisionLevel, bool isNormalInverted)
        {
            var mesh = new Mesh();
            var newObject = new GameObject();
            _spawnedMeshes.Add(newObject);

            /*if (subdivisionLevel > 1)
            {
                var vertices = new Vector3[points.Length + points.Length / 2 * subdivisionLevel];

                for (int i = 0; i < points.Length / 2; i++)
                {
                    for (int j = 1; j < subdivisionLevel; j++)
                    {
                        vertices[i * points.Length / 2 + j] = points[i + 1] - points[i] / j;
                    }
                }
            }      */      

            var xSize = points.Length / 2 - 1;
            var ySize = subdivisionLevel;

            var triangles = new int[xSize * ySize * 6];
            for (int i = 0; i < xSize; i++)
            {
                var triangleIndex = i * 6;
                var vertexIndex = i * 2;
                triangles[triangleIndex] = vertexIndex;
                triangles[triangleIndex + 3] = triangles[triangleIndex + 2] = 
                    isNormalInverted ? vertexIndex + 2 : vertexIndex + 1;
                triangles[triangleIndex + 4] = triangles[triangleIndex + 1] = 
                    isNormalInverted ? vertexIndex + 1 : vertexIndex + 2;
                triangles[triangleIndex + 5] = vertexIndex + 3;
            }
            
            
            mesh.vertices = points;
            mesh.triangles = triangles;
            
            newObject.AddComponent<MeshRenderer>().sharedMaterial = _material;
            newObject.AddComponent<MeshFilter>().mesh = mesh;
        }
    }
}