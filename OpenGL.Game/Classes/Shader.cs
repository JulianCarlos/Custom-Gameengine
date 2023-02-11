using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace OpenGL.Game
{
    public class Shader
    {
        public static Shader DefaultShader = new Shader();

        public int program;

        private string vertexPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Shader", "vert.vs");
        private string fragmentPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Shader", "frag.fs");

        public Shader()
        {
            int vertexShader = CompileShader(vertexPath, ShaderType.VertexShader);
            int fragmentShader = CompileShader(fragmentPath, ShaderType.FragmentShader);

            program = GL.CreateProgram();
            GL.AttachShader(program, vertexShader);
            GL.AttachShader(program, fragmentShader);
            GL.LinkProgram(program);

            int success;
            GL.GetProgram(program, GetProgramParameterName.LinkStatus, out success);
            if (success == 0)
            {
                string infoLog = GL.GetProgramInfoLog(program);
                Console.WriteLine("Error linking program: " + infoLog);
                return;
            }

            GL.DetachShader(program, vertexShader);
            GL.DetachShader(program, fragmentShader);
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);
        }

        public Shader(string vertexPath, string framentPath)
        {
            int vertexShader = CompileShader(vertexPath, ShaderType.VertexShader);
            int fragmentShader = CompileShader(framentPath, ShaderType.FragmentShader);

            program = GL.CreateProgram();
            GL.AttachShader(program, vertexShader);
            GL.AttachShader(program, fragmentShader);
            GL.LinkProgram(program);

            int success;
            GL.GetProgram(program, GetProgramParameterName.LinkStatus, out success);
            if (success == 0)
            {
                string infoLog = GL.GetProgramInfoLog(program);
                Console.WriteLine("Error linking program: " + infoLog);
                return;
            }

            GL.DetachShader(program, vertexShader);
            GL.DetachShader(program, fragmentShader);
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);
        }

        public int GetUniformLocation(string name)
        {
            return GL.GetUniformLocation(program, name);
        }

        private int CompileShader(string path, ShaderType type)
        {
            string shaderSource;
            using (StreamReader reader = new StreamReader(path))
            {
                shaderSource = reader.ReadToEnd();
            }

            int shader = GL.CreateShader(type);
            GL.ShaderSource(shader, shaderSource);
            GL.CompileShader(shader);

            int success;
            GL.GetShader(shader, ShaderParameter.CompileStatus, out success);
            if (success == 0)
            {
                string infoLog = GL.GetShaderInfoLog(shader);
                Console.WriteLine("Error compiling " + type + "shader: " + infoLog);
                return 0;
            }

            return shader;
        }
    }
}
