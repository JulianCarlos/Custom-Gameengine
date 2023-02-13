using OpenGL.Game;
using OpenTK;
using OpenGL.Game.Classes;
using System.ComponentModel;

namespace ConsoleApp1
{
    public class PlayerController : MonoBehaviour
    {
        private Camera ingameCamera = new Camera((1600) / 800, 45f, 0.01f, 100f);

        private float speed = 5;

        public override void Awake()
        {
            base.Awake();

            gameObject.transform.AddChild(ingameCamera.transform);
            ingameCamera.transform.position = new Vector3(0, 2, 0);
        }

        public override void Update()
        {
            base.Update();
            UpdateCameraPosition();
        }

        private void UpdateCameraPosition()
        {
            ingameCamera.transform.Rotate(new Vector3(Input.mouseDelta.Y * 20 * Time.DeltaTime, 0, 0));
            gameObject.transform.Rotate(new Vector3(0, Input.mouseDelta.X * 20 * Time.DeltaTime, 0));
            
            gameObject.transform.position += -gameObject.transform.forward * speed * Input.VerticalInput * Time.DeltaTime;
            gameObject.transform.position += gameObject.transform.right * speed * Input.HorizontalInput * Time.DeltaTime;
            gameObject.transform.position += gameObject.transform.up * speed * Input.UpInput * Time.DeltaTime;
        }
    }
}
