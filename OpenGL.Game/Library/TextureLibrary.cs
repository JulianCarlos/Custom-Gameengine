using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL.Game.Library
{
    public static class TextureLibrary
    {
        public static Texture EmptyTexture = Texture.LoadFromFile(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Texture", "empty.png"));
        public static Texture DefaultTexture = Texture.LoadFromFile(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Texture", "crate.jpg"));
    }
}
