using Microsoft.Win32.SafeHandles;
using OpenGL.Game;
using OpenGL.Game.Library;
using OpenTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL.Custom
{
    public class MapGenerator : MonoBehaviour
    {
        private List<GameObject> cubes = new List<GameObject>();

        public override void Awake()
        {
            base.Awake();

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    GameObject gameObject = GameObject.CreatePrimitives(GameObject.PrimitiveType.cube);
                    gameObject.transform.position = new Vector3(i - 5f, -1, j - 5f);

                    cubes.Add(gameObject);
                }
            }

            cubes[0].transform.position = new Vector3(10, 10, 10);

            cubes[0].transform.AddChild(cubes[4].transform);
            cubes[0].transform.AddChild(cubes[3].transform);
            cubes[0].transform.AddChild(cubes[17].transform);
            cubes[0].transform.AddChild(cubes[12].transform);

            cubes[5].material = MaterialLibrary.emptyMaterial;
            cubes[0].material = MaterialLibrary.emptyMaterial;

            GameObject test = GameObject.CreatePrimitives(GameObject.PrimitiveType.empty);
        }

        public override void Update()
        {
            base.Update();

            cubes[0].transform.Rotate(new Vector3(1, 1, 1) * Time.DeltaTime);
            cubes[17].transform.Rotate(new Vector3(3, 3, 2) * Time.DeltaTime);
            cubes[12].transform.Rotate(new Vector3(1, -3, 2) * Time.DeltaTime);
            cubes[0].transform.scale = Vector3.One * (float)(Math.Sin(Time.time) + 2) / 2;
        }
    }
}
