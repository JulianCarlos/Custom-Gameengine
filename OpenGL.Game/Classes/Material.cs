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
        public static Material defaultMaterial = new Material();

        public Texture texture;
        public Shader shader;

        private Vector3 diffuseColor;
        private Vector3 specularColor;
        private float shininess;

        public Material()
        {
            this.shader = Shader.DefaultShader;
            texture = Texture.DefaultTexture;
        }

        public void Apply()
        {
            if (texture == null)
                return;

            GL.Uniform3(shader.GetUniformLocation("material.diffuse"), diffuseColor);
            GL.Uniform3(shader.GetUniformLocation("material.specular"), specularColor);
            GL.Uniform1(shader.GetUniformLocation("material.shininess"), shininess);
        }

        public void SetDiffuseColor(Vector3 color)
        {
            diffuseColor = color;
        }

        public void SetDiffuseTexture(Texture texture)
        {
            this.texture = texture;
        }

        public void SetSpecularColor(Vector3 color)
        {
            specularColor = color;
        }

        public void SetShininess(float value)
        {
            shininess = value;
        }
    }
}
