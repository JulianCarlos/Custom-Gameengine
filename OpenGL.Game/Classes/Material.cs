using OpenGL.Game.Library;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL.Game
{
    public class Material
    {
        public Texture texture;
        public Shader shader;

        private Vector3 direction = new Vector3(0.1f, 0.5f, 0.5f);
        private Vector3 diffuse = new Vector3(0.8f, 0.8f, 0.8f);
        private Vector3 ambient = new Vector3(0.2f, 0.2f, 0.2f);
        private Vector3 lightColor = new Vector3(0.6f, 0.6f, 0.6f);

        private float shininess = 4f;
        private Vector3 specularColor = new Vector3(0.5f, 0.5f, 0.5f);

        public Material()
        {
            shader = ShaderLibrary.DefaultShader;
            texture = TextureLibrary.DefaultTexture;
        }

        public void Apply()
        {
            if (texture == null)
                return;

            GL.Uniform1(shader.GetUniformLocation("shininess"), shininess);
            GL.Uniform3(shader.GetUniformLocation("specularColor"), specularColor);

            GL.Uniform3(shader.GetUniformLocation("lightDirection"), Scene.sun.forward);
            GL.Uniform3(shader.GetUniformLocation("diffuseColor"), diffuse);
            GL.Uniform3(shader.GetUniformLocation("ambientColor"), ambient);
            GL.Uniform3(shader.GetUniformLocation("lightColor"), lightColor);
        }
    }
}
