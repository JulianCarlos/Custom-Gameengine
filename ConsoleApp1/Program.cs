using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using OpenGL.Game;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using OpenGL.Game.Classes;
using System.Drawing;
using System.IO;

namespace OpenGL.Loop
{
    public class Program : GameWindow
    {
        public static List<GameObject> ObjectsInScene = new List<GameObject>();

        private Camera ingameCamera = new Camera((width) / height, 45f, 0.01f, 100f);
        private Camera editorCamera = new Camera((width) / height, 45f, 0.01f, 100f);

        private List<MonoBehaviour> awakeScripts = new List<MonoBehaviour>();
        private List<MonoBehaviour> lateAwakeScripts = new List<MonoBehaviour>();
        private List<MonoBehaviour> startScripts = new List<MonoBehaviour>();
        private List<MonoBehaviour> updateScripts = new List<MonoBehaviour>();
        private List<MonoBehaviour> lateUpdateScripts = new List<MonoBehaviour>();

        private MethodInfo awakeMethod;
        private MethodInfo lateAwakeMethod;
        private MethodInfo startMethod;
        private MethodInfo updateMethod;
        private MethodInfo lateUpdateMethod;

        private static int width = 1600;
        private static int height = 800;

        private float cameraSpeed = 5;

        private Vector2 mouseDelta;
        private Vector2 currentMousePosition;
        private Vector2 previousMousePosition;

        private KeyboardState keyboardState;
        private MouseState mouseState;

        private Skybox skybox;

        private GameObject player;

        public Program() : base(width, height, GraphicsMode.Default, "My poor Game")
        {
            this.Resize += OnWindowResize;
        }

        private void Game_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Exit();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(Color4.CornflowerBlue);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Texture2D);
            GL.DepthFunc(DepthFunction.Less);

            CursorVisible = false;
            CursorGrabbed = true;

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    GameObject gameObject = GameObject.CreatePrimitives(GameObject.PrimitiveType.cube);
                    gameObject.transform.position = new Vector3(i - 5f, 0, j - 5f);
                    ObjectsInScene.Add(gameObject);
                }
            }

            skybox = new Skybox();

            ObjectsInScene[0].transform.AddChild(ObjectsInScene[5].transform);
            ObjectsInScene[0].transform.AddChild(ObjectsInScene[10].transform);
            ObjectsInScene[0].transform.AddChild(ObjectsInScene[7].transform);
            ObjectsInScene[0].transform.AddChild(ObjectsInScene[25].transform);
            ObjectsInScene[0].transform.AddChild(ObjectsInScene[35].transform);
            ObjectsInScene[0].transform.position += new Vector3(0, 8, 0);
            ObjectsInScene[0].transform.AddChild(ingameCamera.transform);

            ingameCamera.transform.position = new Vector3(0, 1, 0);

            GetAllMonoBehaviours();

            player = GameObject.CreatePrimitives(GameObject.PrimitiveType.cube);
            player.SetActive(false);
            player.transform.position = new Vector3(0, 1, 0);
            player.transform.AddChild(ingameCamera.transform);
            ObjectsInScene.Add(player);

            foreach (MonoBehaviour mono in awakeScripts)
            {
                awakeMethod.Invoke(mono, null);
            }

            foreach (MonoBehaviour mono in lateAwakeScripts)
            {
                lateAwakeMethod.Invoke(mono, null);
            }

            foreach (MonoBehaviour mono in startScripts)
            {
                startMethod.Invoke(mono, null);
            }
        }

        private void OnWindowResize(object sender, EventArgs e)
        {
            width = this.ClientSize.Width;
            height = this.ClientSize.Height;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(Color4.CornflowerBlue);
            
            RenderAllObjectsInScene();

            GL.DepthFunc(DepthFunction.Lequal);
            skybox.DrawSkybox(ingameCamera);
            GL.DepthFunc(DepthFunction.Less);

            UpdateCameraPosition();

            SwapBuffers();
        }

        private void UpdateCameraPosition()
        {
            ingameCamera.transform.Rotate(new Vector3(mouseDelta.Y * 20 * Time.DeltaTime, 0, 0));

            player.transform.Rotate(new Vector3(0, mouseDelta.X * 20 * Time.DeltaTime, 0));

            keyboardState = Keyboard.GetState();
            mouseDelta = CalculateMouseDelta();

            if (keyboardState.IsKeyDown(Key.W))
            {
                player.transform.position += -player.transform.forward * cameraSpeed * Time.DeltaTime;
            }
            else if (keyboardState.IsKeyDown(Key.S))
            {
                player.transform.position += player.transform.forward * cameraSpeed * Time.DeltaTime;
            }
            
            if (keyboardState.IsKeyDown(Key.A))
            {
                player.transform.position += -player.transform.right * cameraSpeed * Time.DeltaTime;
            }
            else if (keyboardState.IsKeyDown(Key.D))
            {
                player.transform.position += player.transform.right * cameraSpeed * Time.DeltaTime;
            }
            
            if (keyboardState.IsKeyDown(Key.Space))
            {
                player.transform.position += player.transform.up * cameraSpeed * Time.DeltaTime;
            }
            else if (keyboardState.IsKeyDown(Key.ControlLeft))
            {
                player.transform.position += -player.transform.up * cameraSpeed * Time.DeltaTime;
            }
        }

        private Vector2 CalculateMouseDelta()
        {
            mouseState = Mouse.GetState();
            int x = mouseState.X * -1;
            int y = mouseState.Y * -1;
            currentMousePosition = new Vector2(x, y);

            mouseDelta = currentMousePosition - previousMousePosition;
            previousMousePosition = currentMousePosition;

            return mouseDelta / 100;
        }

        private void RenderAllObjectsInScene()
        {
            foreach (GameObject game in ObjectsInScene)
            {
                game.meshRenderer.Render(ingameCamera);
            }
            ObjectsInScene[0].transform.Rotate(new Vector3(1 * Time.DeltaTime, 1 * Time.DeltaTime, 1 * Time.DeltaTime));
            ObjectsInScene[5].transform.Rotate(new Vector3(5 * Time.DeltaTime, 5 * Time.DeltaTime, 5 * Time.DeltaTime));
            ObjectsInScene[0].transform.scale = Vector3.One * (float)(Math.Sin(Time.time * 3) + 2) / 2;
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            foreach (MonoBehaviour mono in updateScripts)
            {
                updateMethod.Invoke(mono, null);
            }

            foreach (MonoBehaviour mono in lateUpdateScripts)
            {
                lateUpdateMethod.Invoke(mono, null);
            }

            var keyboard = Keyboard.GetState();
            if (keyboard[Key.Escape])
                Exit();

            GL.GetError();
        }

        [STAThread]
        static void Main()
        {
            using (var game = new Program())
            {
                game.Run(60.0);
            }
        }

        private void GetAllMonoBehaviours()
        {
            Assembly assembly = Assembly.Load("OpenGL.Game");

            var types = assembly.GetTypes()
                .Where(t => t.IsSubclassOf(typeof(MonoBehaviour)));

            foreach (Type type in types)
            {
                awakeMethod = type.GetMethod("Awake", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                lateAwakeMethod = type.GetMethod("LateAwake", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                startMethod = type.GetMethod("Start", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                updateMethod = type.GetMethod("Update", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                lateUpdateMethod = type.GetMethod("LateUpdate", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                if (awakeMethod != null)
                {
                    MonoBehaviour obj = (MonoBehaviour)Activator.CreateInstance(type);
                    awakeScripts.Add(obj);
                }

                if (lateAwakeMethod != null)
                {
                    MonoBehaviour obj = (MonoBehaviour)Activator.CreateInstance(type);
                    lateAwakeScripts.Add(obj);
                }

                if (startMethod != null)
                {
                    MonoBehaviour obj = (MonoBehaviour)Activator.CreateInstance(type);
                    startScripts.Add(obj);
                }

                if (updateMethod != null)
                {
                    MonoBehaviour obj = (MonoBehaviour)Activator.CreateInstance(type);
                    updateScripts.Add(obj);
                }

                if (lateUpdateMethod != null)
                {
                    MonoBehaviour obj = (MonoBehaviour)Activator.CreateInstance(type);
                    lateUpdateScripts.Add(obj);
                }
            }
        }
    }
}


