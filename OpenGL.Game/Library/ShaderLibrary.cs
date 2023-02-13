using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL.Game.Library
{
    public static class ShaderLibrary
    {
        public static Shader DefaultShader;
        private static string defaultVertexPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Shader", "vert.vs");
        private static string defaultFragPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Shader", "frag.fs");

        public static Shader DirectionalShader;
        private static string directionalVertexPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Shader", "directionalVert.vs");
        private static string directionalFragPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Shader", "directionalFrag.fs");

        public static Shader CrazyCircleShader;
        private static string crazyCircleVertexPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Shader", "edgeVert.vs");
        private static string crazyCircleFragPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Shader", "edgeFrag.fs");

        public static void GenerateShaders()
        {
            DefaultShader = new Shader(defaultVertexPath, defaultFragPath);
            DirectionalShader = new Shader(directionalVertexPath, directionalFragPath);
            CrazyCircleShader = new Shader(crazyCircleVertexPath, crazyCircleFragPath);
        }
    }
}
