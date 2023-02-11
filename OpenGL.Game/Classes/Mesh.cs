using OpenTK;
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

        public Mesh(Vector3[] vertices, Vector2[] uvs, Vector3[] normals, uint[] indices)
        {
            this.Vertices = vertices;
            this.Uvs = uvs;
            this.Normals = normals;
            this.Indices = indices;
        }
    }
}
