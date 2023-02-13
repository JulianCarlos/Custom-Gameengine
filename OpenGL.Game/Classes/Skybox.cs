using OpenTK;
using OpenTK.Graphics.OpenGL;
using StbImageSharp;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using PixelFormat = System.Drawing.Imaging.PixelFormat;
using ImageMagick;
using OpenGL.Game.Library;

namespace OpenGL.Game.Classes
{
    public class Skybox
    {
        private string frontSide = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Texture", "SkyBox_PZ.png");
        private string backSide = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Texture", "SkyBox_NZ.png");
        private string leftSide = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Texture", "SkyBox_PX.png");
        private string rightSide = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Texture", "SkyBox_NX.png");
        private string bottomSide = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Texture", "SkyBox_NY.png");
        private string topSide = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Texture", "SkyBox_PY.png");

        private string[] fileNames;
        private uint textureID;
        private int shaderID;

        private Vector3[] vertices =
        {
            new Vector3(-1.0f, -1.0f, 1.0f),
            new Vector3(1.0f, -1.0f, 1.0f),
            new Vector3(1.0f, -1.0f, -1.0f),
            new Vector3(-1.0f, -1.0f, -1.0f),
            new Vector3(-1.0f, 1.0f, 1.0f),
            new Vector3(1.0f, 1.0f, 1.0f),
            new Vector3(1.0f, 1.0f, -1.0f),
            new Vector3(-1.0f, 1.0f, -1.0f)
        };
        private uint[] indices =
        {
            // Right
            1, 2, 6,
            6, 5, 1,

            // Left
            0, 4, 7,
            7, 3, 0,

            // Top
            4, 5, 6,
            6, 7, 4,

            // Bottom
            0, 3, 2,
            2, 1, 0,

            // Back
            0, 1, 5,
            5, 4, 0,

            // Front
            3, 7, 6,
            6, 2, 3
        };

        private uint VAO;
        private uint VBO;
        private uint EBO;

        public Skybox()
        {
            LoadSkyBox(leftSide, rightSide, topSide, bottomSide, frontSide, backSide);
        }

        public Skybox(string PosX, string NegX, string PosY, string NegY, string PosZ, string NegZ)
        {
            LoadSkyBox(PosX, NegX, PosY, NegY, PosZ, NegZ);
        }

        public bool LoadSkyBox(string PosX, string NegX, string PosY, string NegY, string PosZ, string NegZ)
        {
            fileNames = new string[6] {PosX, NegX, PosY, NegY, PosZ, NegZ};
            shaderID = ShaderLibrary.SkyBoxShader.program;

            GL.GenVertexArrays(1, out VAO);
            GL.GenBuffers(1, out VBO);
            GL.GenBuffers(1, out EBO);
            GL.BindVertexArray(VAO);

            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * 3 * vertices.Length, vertices, BufferUsageHint.StaticDraw);
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, sizeof(uint) * indices.Length, indices, BufferUsageHint.StaticDraw);

            GL.GenTextures(1, out textureID);
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.TextureCubeMap, textureID);

            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapR, (int)TextureWrapMode.ClampToEdge);

            for (int i = 0; i < fileNames.Length; i++)
            {
                using (MagickImage image = new MagickImage(fileNames[i]))
                {
                    byte[] blob = image.ToByteArray(MagickFormat.Rgba);
            
                    GCHandle handle = GCHandle.Alloc(blob, GCHandleType.Pinned);
            
                    try
                    {
                        GL.TexImage2D(TextureTarget.TextureCubeMapPositiveX + i, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Rgba, PixelType.UnsignedByte, handle.AddrOfPinnedObject());
                    }
                    finally
                    {
                        handle.Free();
                    }
                }
            }

            return true;
        }

        public void DrawSkybox(Camera camera)
        {
            if (camera == null)
                return;

            GL.UseProgram(shaderID);

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.TextureCubeMap, textureID);

            // Send the view matrix to the vertex shader
            int viewMatrixUniformLocation = GL.GetUniformLocation(shaderID, "view");
            Matrix4 matrix4 = camera.GetViewMatrix();

            for (int i = 0; i < 4; i++)
            {
                matrix4[3, i] = 0.0f;
            }
            for (int i = 0; i < 4; i++)
            {
                matrix4[i, 3] = 0.0f;
            }

            GL.UniformMatrix4(viewMatrixUniformLocation, false, ref matrix4);

            // Send the projection matrix to the vertex shader
            int projectionMatrixUniformLocation = GL.GetUniformLocation(shaderID, "projection");
            matrix4 = camera.GetProjectionMatrix();
            GL.UniformMatrix4(projectionMatrixUniformLocation, false, ref matrix4);

            GL.BindVertexArray(VAO);
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
        }
    }
}
