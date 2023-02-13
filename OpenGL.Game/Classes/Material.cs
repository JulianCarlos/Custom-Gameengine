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
using System.Xml.Linq;

namespace OpenGL.Game
{
    public class Material
    {
        public int TextureID = -1;
        public int ShaderID = -1;

        private Vector3 direction = new Vector3(0.1f, 0.5f, 0.5f);
        private Vector3 diffuse = new Vector3(0.8f, 0.8f, 0.8f);
        private Vector3 ambient = new Vector3(0.2f, 0.2f, 0.2f);
        private Vector3 lightColor = new Vector3(0.6f, 0.6f, 0.6f);

        private float shininess = 4f;
        private Vector3 specularColor = new Vector3(0.5f, 0.5f, 0.5f);

        public Material()
        {
            ShaderID = ShaderLibrary.DefaultShader.program;
            TextureID = TextureLibrary.DefaultTexture.Id;
        }

        public Material(Texture texture)
        {
            ShaderID = ShaderLibrary.DefaultShader.program;
            TextureID = texture.Id;
        }

        public int GetUniformLocation(string name)
        {
            return GL.GetUniformLocation(ShaderID, name);
        }

        public void Apply()
        {
            if (TextureID == -1 || ShaderID == -1)
                return;


            GL.Uniform1(GetUniformLocation("shininess"), shininess);
            GL.Uniform3(GetUniformLocation("specularColor"), specularColor);
            
            GL.Uniform3(GetUniformLocation("lightDirection"), Scene.DirectionalLight.forward);
            GL.Uniform3(GetUniformLocation("diffuseColor"), diffuse);
            GL.Uniform3(GetUniformLocation("ambientColor"), ambient);
            GL.Uniform3(GetUniformLocation("lightColor"), lightColor);
        }
    }
}
