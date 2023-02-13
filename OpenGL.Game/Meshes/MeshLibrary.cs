using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL.Game.Meshes
{
    public static class MeshLibrary
    {
        public static Mesh DefaultCubeMesh;

        public static void GenerateMeshes()
        {
            var cubeStruct = new CubeStruct(0);
            DefaultCubeMesh = new Mesh(cubeStruct.Vertices, cubeStruct.Uvs, cubeStruct.Normals, cubeStruct.Indices);
        }

        public struct CubeStruct
        {
            public Vector3[] Vertices;
            public Vector3[] Normals;
            public Vector2[] Uvs;
            public uint[] Indices;

            public CubeStruct(float size)
            {
                Vertices = new Vector3[]
                {
                    //Front
                    new Vector3(-0.5f, -0.5f, 0.5f),
                    new Vector3(0.5f, -0.5f, 0.5f),
                    new Vector3(-0.5f, 0.5f, 0.5f),
                    new Vector3(0.5f, 0.5f, 0.5f),
                    
                    //Back
                    new Vector3(-0.5f, -0.5f, -0.5f),
                    new Vector3(0.5f, -0.5f, -0.5f),
                    new Vector3(-0.5f, 0.5f, -0.5f),
                    new Vector3(0.5f, 0.5f, -0.5f),

                    //Right
                    new Vector3(0.5f, -0.5f, -0.5f),
                    new Vector3(0.5f, -0.5f, 0.5f),
                    new Vector3(0.5f, 0.5f, -0.5f),
                    new Vector3(0.5f, 0.5f, 0.5f),

                    //Left
                    new Vector3(-0.5f, -0.5f, -0.5f),
                    new Vector3(-0.5f, -0.5f, 0.5f),
                    new Vector3(-0.5f, 0.5f, -0.5f),
                    new Vector3(-0.5f, 0.5f, 0.5f),

                    //Top
                    new Vector3(-0.5f, 0.5f, -0.5f),
                    new Vector3(0.5f, 0.5f, -0.5f),
                    new Vector3(-0.5f, 0.5f, 0.5f),
                    new Vector3(0.5f, 0.5f, 0.5f),

                    //Bottom
                    new Vector3(-0.5f, -0.5f, -0.5f),
                    new Vector3(0.5f, -0.5f, -0.5f),
                    new Vector3(-0.5f, -0.5f, 0.5f),
                    new Vector3(0.5f, -0.5f, 0.5f)
                };

                Normals = new Vector3[]
                {
                    new Vector3(0.0f, 0.0f, 1.0f),
                    new Vector3(0.0f, 0.0f, 1.0f),
                    new Vector3(0.0f, 0.0f, 1.0f),
                    new Vector3(0.0f, 0.0f, 1.0f),
                    new Vector3(0.0f, 0.0f, -1.0f),
                    new Vector3(0.0f, 0.0f, -1.0f),
                    new Vector3(0.0f, 0.0f, -1.0f),
                    new Vector3(0.0f, 0.0f, -1.0f),
                    new Vector3(0.0f, -1.0f, 0.0f),
                    new Vector3(0.0f, -1.0f, 0.0f),
                    new Vector3(0.0f, -1.0f, 0.0f),
                    new Vector3(0.0f, -1.0f, 0.0f),
                    new Vector3(0.0f, 1.0f, 0.0f),
                    new Vector3(0.0f, 1.0f, 0.0f),
                    new Vector3(0.0f, 1.0f, 0.0f),
                    new Vector3(0.0f, 1.0f, 0.0f),
                };

                Uvs = new Vector2[]
                {
                    new Vector2(0f,0f),
                    new Vector2(1f,0f),
                    new Vector2(0f,1f),
                    new Vector2(1f,1f),

                    new Vector2(0f,0f),
                    new Vector2(1f,0f),
                    new Vector2(0f,1f),
                    new Vector2(1f,1f),

                    new Vector2(0f,0f),
                    new Vector2(1f,0f),
                    new Vector2(0f,1f),
                    new Vector2(1f,1f),

                    new Vector2(0f,0f),
                    new Vector2(1f,0f),
                    new Vector2(0f,1f),
                    new Vector2(1f,1f),

                    new Vector2(0f,0f),
                    new Vector2(1f,0f),
                    new Vector2(0f,1f),
                    new Vector2(1f,1f),

                    new Vector2(0f,0f),
                    new Vector2(1f,0f),
                    new Vector2(0f,1f),
                    new Vector2(1f,1f)
                };

                Indices = new uint[]
                {
                      // Front Face
                      0, 1, 2, // First Triangle
                      1, 3, 2, // Second Triangle
                
                      // Back Face
                      2 + 4, 1 + 4, 0 + 4, // First Triangle
                      3 + 4, 1 + 4, 2 + 4, // Second Triangle
                
                      // Right Face
                      2 + 8, 1 + 8, 0 + 8, // First Triangle
                      3 + 8, 1 + 8, 2 + 8, // Second Triangle
                
                      // Left Face
                      0 + 12, 1 + 12, 2 + 12, // First Triangle
                      1 + 12, 3 + 12, 2 + 12, // Second Triangle
                
                      // Top Face
                      2 + 16, 1 + 16, 0 + 16, // First Triangle
                      3 + 16, 1 + 16, 2 + 16, // Second Triangle
                
                      // Bottom Face
                      0 + 20, 1 + 20, 2 + 20, // First Triangle
                      1 + 20, 3 + 20, 2 + 20, // Second Triangle
                };
            }
        }
    }
}
