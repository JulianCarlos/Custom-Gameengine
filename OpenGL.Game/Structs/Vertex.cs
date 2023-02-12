using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL.Game.Structs
{
    public struct Vertex
    {
        public Vector3 Position;
        public Vector4 Color;
        public Vector2 TexCoords;
        public uint TexID;
    }
}
