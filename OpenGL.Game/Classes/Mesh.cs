using Assimp;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL.Game
{
    public class Mesh
    {
        public Vector3[] Vertices;
        public Vector2[] Uvs;
        public Vector3[] Normals;
        public uint[] Indices;

        private int VBO;
        private int VAO;
        private int EBO;
        private int NBO;
        private int TBO;

        public Mesh(Vector3[] vertices, Vector2[] uvs, Vector3[] normals, uint[] indices)
        {
            Vertices = vertices;
            Uvs = uvs;
            Normals = normals;
            Indices = indices;

            VAO = GL.GenVertexArray();
            GL.BindVertexArray(VAO);

            VBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Length * sizeof(float) * 3, Vertices, BufferUsageHint.DynamicDraw);
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);

            NBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, NBO);
            GL.BufferData(BufferTarget.ArrayBuffer, Normals.Length * sizeof(float) * 3, Normals, BufferUsageHint.StaticDraw);
            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 0, 0);

            TBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, TBO);
            GL.BufferData(BufferTarget.ArrayBuffer, Uvs.Length * sizeof(float) * 2, Uvs, BufferUsageHint.StaticDraw);
            GL.EnableVertexAttribArray(2);
            GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 0, 0);

            EBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, Indices.Length * sizeof(int), Indices, BufferUsageHint.StaticDraw);
        }

        public void BindMesh()
        {
            GL.BindVertexArray(VAO);
        }
    }
}
