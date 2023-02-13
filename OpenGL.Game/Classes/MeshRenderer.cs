using OpenGL.Game.AbstractClasses;
using OpenGL.Game.Classes;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using static OpenTK.Graphics.OpenGL.GL;

namespace OpenGL.Game
{
    public class MeshRenderer : Component
    {
        private Mesh mesh;

        private int ShaderID;
        private int TextureID;

        public MeshRenderer(Mesh mesh, GameObject gameObject)
        {
            Name = "MeshRenderer";
            this.mesh = mesh;
            this.gameObject = gameObject;

            ShaderID = gameObject.material.ShaderID;
            TextureID = gameObject.material.TextureID;

            var ligthingShaderPosition = gameObject.material.GetUniformLocation("aPos");
            GL.EnableVertexAttribArray(ligthingShaderPosition);
            GL.VertexAttribPointer(ligthingShaderPosition, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
            
            var normalLocation = gameObject.material.GetUniformLocation("aNormal");
            GL.EnableVertexAttribArray(normalLocation);
            GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));
            
            var texCoordLocation = gameObject.material.GetUniformLocation("aTexCoord");
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 0, 0);
        }

        public override void Update()
        {
            base.Update();

            Render(Scene.Cameras[0]);
        }

        public void Render(Camera camera)
        {
            if (camera == null || mesh == null)
                return;

            if (mesh == null || gameObject.Active == false || (gameObject.transform.Parent != null && gameObject.transform.Parent.gameObject.Active == false))
                return;
            
            GL.UseProgram(ShaderID);

            // Bind the texture
            GL.ActiveTexture(TextureUnit.Texture0);
            gameObject.material.Apply();
            GL.BindTexture(TextureTarget.Texture2D, TextureID);

            // Send the texture uniform to the fragment shader
            int textureUniformLocation = GL.GetUniformLocation(ShaderID, "tex");
            GL.Uniform1(textureUniformLocation, 0);

            // Send the model matrix to the vertex shader
            int modelMatrixUniformLocation = GL.GetUniformLocation(ShaderID, "model");

            Matrix4 matrix4 = gameObject.transform.GetMatrix();
            GL.UniformMatrix4(modelMatrixUniformLocation, false, ref matrix4);

            // Send the view matrix to the vertex shader
            int viewMatrixUniformLocation = GL.GetUniformLocation(ShaderID, "view");
            matrix4 = camera.GetViewMatrix();
            GL.UniformMatrix4(viewMatrixUniformLocation, false, ref matrix4);

            // Send the projection matrix to the vertex shader
            int projectionMatrixUniformLocation = GL.GetUniformLocation(ShaderID, "projection");
            matrix4 = camera.GetProjectionMatrix();
            GL.UniformMatrix4(projectionMatrixUniformLocation, false, ref matrix4);

            mesh.BindMesh();

            GL.DrawElements(BeginMode.Triangles, mesh.Indices.Length, DrawElementsType.UnsignedInt, 0);
        }
    }
}
