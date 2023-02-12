using OpenGL.Game.Classes;
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

        public static void AddObject(Transform transform)
        {
            TransformsInScene.Add(transform);
        }

        public static void UpdateScene()
        {
            foreach (var item in TransformsInScene)
            {
                if(item?.gameObject?.meshRenderer != null && Scene.Cameras.Count > 0)
                {
                    item.gameObject.meshRenderer.Render(Cameras[0]);
                }
            }
        }
    }
}
