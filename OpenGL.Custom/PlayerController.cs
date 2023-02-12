using OpenGL.Game;
using OpenTK;
using OpenGL.Game.Classes;

namespace ConsoleApp1
{
    public class PlayerController : MonoBehaviour
    {
        private GameObject player;
        private Camera ingameCamera = new Camera((1600) / 800, 45f, 0.01f, 100f);

        private float speed = 5;

        public override void Awake()
        {
            //ingameCamera = ;
            player = GameObject.CreatePrimitives(GameObject.PrimitiveType.cube);
            player.SetActive(false);
            player.transform.position = new Vector3(0, 1, 0);
            player.transform.AddChild(ingameCamera.transform);

            System.Console.WriteLine("Test");

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    GameObject gameObject = GameObject.CreatePrimitives(GameObject.PrimitiveType.cube);
                    gameObject.transform.position = new Vector3(i - 5f, 0, j - 5f);
                }
            }
        }

        public override void Update()
        {
            base.Update();
            UpdateCameraPosition();
        }

        private void UpdateCameraPosition()
        {
            //ingameCamera?.transform.Rotate(new Vector3(Input.mouseDelta.Y * 20 * Time.DeltaTime, 0, 0));
            //player?.transform.Rotate(new Vector3(0, Input.mouseDelta.X * 20 * Time.DeltaTime, 0));
            //
            //player.transform.position += -player.transform.forward * speed * Input.VerticalInput * Time.DeltaTime;
            //player.transform.position += player.transform.right * speed * Input.HorizontalInput * Time.DeltaTime;
            //player.transform.position += player.transform.up * speed * Input.UpInput * Time.DeltaTime;
            //
            //ingameCamera.transform.position += new Vector3(0, 1 * Time.DeltaTime, 0);
            //Console.WriteLine(ingameCamera.transform.position);

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
