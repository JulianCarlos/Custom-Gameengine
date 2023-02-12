using OpenGL.Game;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGL.Game.Classes;
using OpenGL.Loop;

namespace ConsoleApp1
{
    public class PlayerController : MonoBehaviour
    {
        private GameObject player;
        private Camera ingameCamera;

        private float speed = 5;

        public override void Awake()
        {
            base.Awake();

            ingameCamera = new Camera((Program.width) / Program.height, 45f, 0.01f, 100f);

            player = GameObject.CreatePrimitives(GameObject.PrimitiveType.cube);
            player.SetActive(false);
            player.transform.position = new Vector3(0, 1, 0);
            player.transform.AddChild(ingameCamera.transform);
        }

        public override void Update()
        {
            base.Update();

            UpdateCameraPosition();
        }

        private void UpdateCameraPosition()
        {
            ingameCamera.transform.Rotate(new Vector3(Input.mouseDelta.Y * 20 * Time.DeltaTime, 0, 0));
            player.transform.Rotate(new Vector3(0, Input.mouseDelta.X * 20 * Time.DeltaTime, 0));
            //
            //keyboardState = Keyboard.GetState();
            //mouseDelta = CalculateMouseDelta();
            //
            //if (keyboardState.IsKeyDown(Key.W))
            //{
            //    player.transform.position += -player.transform.forward * speed * Time.DeltaTime;
            //}
            //else if (keyboardState.IsKeyDown(Key.S))
            //{
            //    player.transform.position += player.transform.forward * speed * Time.DeltaTime;
            //}
            //
            //if (keyboardState.IsKeyDown(Key.A))
            //{
            //    player.transform.position += -player.transform.right * speed * Time.DeltaTime;
            //}
            //else if (keyboardState.IsKeyDown(Key.D))
            //{
            //    player.transform.position += player.transform.right * speed * Time.DeltaTime;
            //}
            //
            //if (keyboardState.IsKeyDown(Key.Space))
            //{
            //    player.transform.position += player.transform.up * speed * Time.DeltaTime;
            //}
            //else if (keyboardState.IsKeyDown(Key.ControlLeft))
            //{
            //    player.transform.position += -player.transform.up * speed * Time.DeltaTime;
            //}
        }
    }
}
