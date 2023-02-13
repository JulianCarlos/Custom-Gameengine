using OpenGL.Game.Classes;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL.Game
{
    public static class Scene
    {
        public static List<Camera> Cameras = new List<Camera>();
        public static List<Transform> TransformsInScene = new List<Transform>();

        private static List<Transform> QueuedTransforms = new List<Transform>();

        public static Transform sun = new Transform();

        public static void AddObject(Transform transform)
        {
            QueuedTransforms.Add(transform);
        }

        public static void StartScene()
        {
            foreach (var item in TransformsInScene)
            {
                item.gameObject.StartAllComponents();
            }
        }

        public static void UpdateScene()
        {
            if (QueuedTransforms.Count > 0)
            {
                TransformsInScene.AddRange(QueuedTransforms);
                QueuedTransforms.Clear();
            }

            foreach (var item in TransformsInScene)
            {
                item.gameObject.UpdateAllComponents();
            }

            sun.Rotate(new Vector3(0.1f, 0.1f, 0) * Time.DeltaTime);
        }
    }
}
