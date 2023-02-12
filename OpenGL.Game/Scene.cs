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
        public static List<Transform> TransformsInScene = new List<Transform>();

        public static void AddObject(Transform transform)
        {
            TransformsInScene.Add(transform);
        }

        public static void UpdateScene(Camera camera)
        {
            foreach (var item in TransformsInScene)
            {
                if(item.gameObject.meshRenderer != null)
                {
                    item.gameObject.meshRenderer.Render(camera);
                }
            }
        }
    }
}
