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

        private int vbo;
        private int vao;
        private int ebo;
        private int nbo;
        private int tbo;

        public MeshRenderer(Mesh mesh, GameObject gameObject)
        {
            this.gameObject = gameObject;
            this.mesh = mesh;

            #region GenerateBuffers
            vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, mesh.Vertices.Length * sizeof(float) * 3, mesh.Vertices, BufferUsageHint.StaticDraw);
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);

            nbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, nbo);
            GL.BufferData(BufferTarget.ArrayBuffer, mesh.Normals.Length * sizeof(float) * 3, mesh.Normals, BufferUsageHint.StaticDraw);
            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 0, 0);

            tbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, tbo);
            GL.BufferData(BufferTarget.ArrayBuffer, mesh.Uvs.Length * sizeof(float) * 2, mesh.Uvs, BufferUsageHint.StaticDraw);
            GL.EnableVertexAttribArray(2);
            GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 0, 0);

            ebo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
            GL.BufferData(BufferTarget.ElementArrayBuffer, mesh.Indices.Length * sizeof(int), mesh.Indices, BufferUsageHint.StaticDraw);

            GL.DrawElements(PrimitiveType.Triangles, mesh.Indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
            #endregion

            var ligthingShaderPosition = gameObject.material.shader.GetUniformLocation("aPos");
            GL.EnableVertexAttribArray(ligthingShaderPosition);
            GL.VertexAttribPointer(ligthingShaderPosition, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
            
            var normalLocation = gameObject.material.shader.GetUniformLocation("aNormal");
            GL.EnableVertexAttribArray(normalLocation);
            GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));
            
            var texCoordLocation = gameObject.material.shader.GetUniformLocation("aTexCoord");
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 0, 0);
        }

        public void Render(Camera camera)
        {
            if (mesh == null || gameObject.Active == false || (gameObject.transform.Parent != null && gameObject.transform.Parent.gameObject.Active == false))
                return;
            
            GL.UseProgram(gameObject.material.shader.program);

            // Bind the texture
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, gameObject.material.texture.Id);

            // Send the texture uniform to the fragment shader
            int textureUniformLocation = GL.GetUniformLocation(gameObject.material.shader.program, "tex");
            GL.Uniform1(textureUniformLocation, 0);

            // Send the model matrix to the vertex shader
            int modelMatrixUniformLocation = GL.GetUniformLocation(gameObject.material.shader.program, "model");

            Matrix4 matrix4 = gameObject.transform.GetMatrix();
            GL.UniformMatrix4(modelMatrixUniformLocation, false, ref matrix4);

            // Send the view matrix to the vertex shader
            int viewMatrixUniformLocation = GL.GetUniformLocation(gameObject.material.shader.program, "view");
            matrix4 = camera.GetViewMatrix();
            GL.UniformMatrix4(viewMatrixUniformLocation, false, ref matrix4);

            // Send the projection matrix to the vertex shader
            int projectionMatrixUniformLocation = GL.GetUniformLocation(gameObject.material.shader.program, "projection");
            matrix4 = camera.GetProjectionMatrix();
            GL.UniformMatrix4(projectionMatrixUniformLocation, false, ref matrix4);

            GL.BindVertexArray(vao);
            //GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
            GL.DrawElements(BeginMode.Triangles, mesh.Indices.Length, DrawElementsType.UnsignedInt, 0);
        }
    }
}
