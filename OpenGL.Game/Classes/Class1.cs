using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL.Game.Classes
{
    internal class Class1
    {
        //-0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 0.0f,
        // 0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 0.0f,
        // 0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 1.0f,
        // 0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 1.0f,
        //-0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 1.0f,
        //-0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 0.0f,
        //
        //-0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 0.0f,
        // 0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 0.0f,
        // 0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 1.0f,
        // 0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 1.0f,
        //-0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 1.0f,
        //-0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 0.0f,
        //
        //-0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 0.0f,
        //-0.5f,  0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 1.0f,
        //-0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
        //-0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
        //-0.5f, -0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 0.0f,
        //-0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 0.0f,
        // 0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 0.0f,
        // 0.5f,  0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 1.0f,
        // 0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
        // 0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
        // 0.5f, -0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 0.0f,
        // 0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 0.0f,
        //
        //-0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 1.0f,
        // 0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 1.0f,
        // 0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 0.0f,
        // 0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 0.0f,
        //-0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 0.0f,
        //-0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 1.0f,
        //
        //-0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 1.0f,
        // 0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 1.0f,
        // 0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 0.0f,
        // 0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 0.0f,
        //-0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 0.0f,
        //-0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 1.0f

        //Vector2[] texCoords = new Vector2[]
        //{
        //    new Vector2(0.0f, 0.0f),
        //    new Vector2(1.0f, 0.0f),
        //    new Vector2(1.0f, 1.0f),
        //    new Vector2(1.0f, 1.0f),
        //    new Vector2(0.0f, 1.0f),
        //    new Vector2(0.0f, 0.0f),
        //
        //    new Vector2(0.0f, 0.0f),
        //    new Vector2(1.0f, 0.0f),
        //    new Vector2(1.0f, 1.0f),
        //    new Vector2(1.0f, 1.0f),
        //    new Vector2(0.0f, 1.0f),
        //    new Vector2(0.0f, 0.0f),
        //
        //    new Vector2(1.0f, 0.0f),
        //    new Vector2(1.0f, 1.0f),
        //    new Vector2(0.0f, 1.0f),
        //    new Vector2(0.0f, 1.0f),
        //    new Vector2(0.0f, 0.0f),
        //    new Vector2(1.0f, 0.0f),
        //
        //    new Vector2(1.0f, 0.0f),
        //    new Vector2(1.0f, 1.0f),
        //    new Vector2(0.0f, 1.0f),
        //    new Vector2(0.0f, 1.0f),
        //    new Vector2(0.0f, 0.0f),
        //    new Vector2(1.0f, 0.0f),
        //
        //    new Vector2(0.0f, 1.0f),
        //    new Vector2(1.0f, 1.0f),
        //    new Vector2(1.0f, 0.0f),
        //    new Vector2(1.0f, 0.0f),
        //    new Vector2(0.0f, 0.0f),
        //    new Vector2(0.0f, 1.0f),
        //
        //    new Vector2(0.0f, 1.0f),
        //    new Vector2(1.0f, 1.0f),
        //    new Vector2(1.0f, 0.0f),
        //    new Vector2(1.0f, 0.0f),
        //    new Vector2(0.0f, 0.0f),
        //    new Vector2(0.0f, 1.0f)
        //};
    }
}
