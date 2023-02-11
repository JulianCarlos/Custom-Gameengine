using OpenGL.Game.AbstractClasses;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL.Game
{
    public class Transform : Component
    {
        public Matrix4 TransformMatrix
        {
            get { return GetMatrix(); }
            set { }
        }

        public Vector3 position { get; set; }
        public Vector3 localPosition { get; set; }
        public Quaternion rotation { get; set; }
        public Quaternion localRotation { get; set; }
        public Vector3 scale { get; set; }

        public Vector3 forward { get { return rotation * Vector3.UnitZ; } }
        public Vector3 up { get { return rotation * Vector3.UnitY; } }
        public Vector3 right { get { return rotation * Vector3.UnitX; } }

        public Vector3 Position
        {
            get { return position; }
            set { position = value; } 
        }
        public Vector3 LocalPosition
        {
            get { return localPosition; }
            set
            {
                localPosition = value;
                position = localPosition + Vector3.Transform(Vector3.Zero, Rotation);
            }
        }

        public Quaternion Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }
        public Quaternion LocalRotation
        {
            get { return localRotation; }
            set
            {
                localRotation = value;
                rotation = localRotation * rotation;
            }
        }

        public Transform Parent;
        public List<Transform> Childs;

        public Transform()
        {
            position = Vector3.Zero;
            rotation = Quaternion.Identity;
            scale = Vector3.One;
        }

        public Vector3 TransformDirection(Vector3 direction)
        {
            return Vector3.Transform(direction, Quaternion.Normalize(rotation));
        }

        public Vector3 InverseTransformDirection(Vector3 direction)
        {
            return Vector3.Transform(direction, Quaternion.Invert(Quaternion.Normalize(rotation)));
        }

        public Matrix4 GetMatrix()
        {
            if(gameObject != null && gameObject.Parent != null)
            {
                Matrix4 translationMatrix = Matrix4.CreateTranslation(position);
                Matrix4 rotationMatrix = Matrix4.CreateFromQuaternion(rotation);
                Matrix4 scaleMatrix = Matrix4.CreateScale(scale);

                var localMatrix = scaleMatrix * rotationMatrix * translationMatrix;

                var matrix = Matrix4.Mult(localMatrix, gameObject.Parent.transform.TransformMatrix);

                return matrix;
            }
            else
            {
                Matrix4 translationMatrix = Matrix4.CreateTranslation(position);
                Matrix4 rotationMatrix = Matrix4.CreateFromQuaternion(rotation);
                Matrix4 scaleMatrix = Matrix4.CreateScale(scale);

                var matrix = scaleMatrix * rotationMatrix * translationMatrix;

                return matrix;
            }
        }

        public void Translate(Vector3 delta)
        {
            position += delta;
        }

        public void Rotate(Vector3 eulerAngles)
        {
            rotation *= Quaternion.FromEulerAngles(eulerAngles);
        }

        public void RotateLocal(Vector3 eulerAngles)
        {
            LocalRotation *= Quaternion.FromEulerAngles(eulerAngles);
        }

        public void Rotate(Quaternion rotation)
        {
            this.rotation *= rotation;
        }

        public void Scale(Vector3 scale)
        {
            this.scale *= scale;
        }
    }
}
