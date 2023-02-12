using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL.Game.Helper
{
    public static class IndincesCalculator
    {
        public static uint[] CalculateIndices(int vertices, uint offset)
        {
            List<uint> indices = new List<uint>()
            {
                  // Front Face
                  0 + offset, 1 + offset, 2 + offset, // First Triangle
                  1 + offset, 3 + offset, 2 + offset, // Second Triangle

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

            return indices.ToArray();
        }

        public static uint[] GenerateIndices(uint numVertices, uint startVertexIndex)
        {
            uint[] indices = new uint[numVertices * 6];
            for (int i = 0; i < 6; i++)
            {
                indices[i * 6 + 0] = startVertexIndex + 0;
                indices[i * 6 + 1] = startVertexIndex + 1;
                indices[i * 6 + 2] = startVertexIndex + 2;
                indices[i * 6 + 3] = startVertexIndex + 1;
                indices[i * 6 + 4] = startVertexIndex + 3;
                indices[i * 6 + 5] = startVertexIndex + 2;

                startVertexIndex += 4;
            }

            return indices;
        }
    }
}
