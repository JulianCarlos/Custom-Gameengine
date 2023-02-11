using OpenGL.Game.AbstractClasses;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL.Game.Classes
{
    public class Camera : Component
    {
        public Transform transform = new Transform();

        private Matrix4 projectionMatrix;

        private float nearClipping;
        private float farClipping;

        public Camera(float aspectRatio, float fov, float nearClip, float farClip)
        {
            this.projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(fov), aspectRatio, nearClip, farClip);

            nearClipping = nearClip;
            farClipping = farClip;
        }

        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt(transform.position, transform.position + transform.forward, transform.up);
        }

        public Matrix4 GetProjectionMatrix()
        {
            return projectionMatrix;
        }
    }
}
