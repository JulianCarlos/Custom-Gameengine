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

        public static int width = 1600;
        public static int height = 800;

        private Skybox skybox;

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

            GetAllMonoBehaviours();

            //GL.Enable(EnableCap.CullFace);
            //GL.CullFace(CullFaceMode.Back);

            CursorVisible = false;
            CursorGrabbed = true;

            //for (int i = 0; i < 10; i++)
            //{
            //    for (int j = 0; j < 10; j++)
            //    {
            //        GameObject gameObject = GameObject.CreatePrimitives(GameObject.PrimitiveType.cube);
            //        gameObject.transform.position = new Vector3(i - 5f, 0, j - 5f);
            //    }
            //}

            skybox = new Skybox();

            //ingameCamera.transform.position = new Vector3(0, 1, 0);

            //player = GameObject.CreatePrimitives(GameObject.PrimitiveType.cube);
            //player.SetActive(false);
            //player.transform.position = new Vector3(0, 1, 0);
            //player.transform.AddChild(ingameCamera.transform);

            foreach (MonoBehaviour mono in awakeScripts)
            {
                mono.Awake();
            }

            foreach (MonoBehaviour mono in lateAwakeScripts)
            {
                mono.LateAwake();
            }

            foreach (MonoBehaviour mono in startScripts)
            {
                mono.Start();
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

            Input.UpdateInput();
            Scene.UpdateScene();

            GL.DepthFunc(DepthFunction.Lequal);
            skybox.DrawSkybox(Scene.Cameras[0]);
            GL.DepthFunc(DepthFunction.Less);

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            foreach (MonoBehaviour mono in updateScripts)
            {
                mono.Update();
            }

            foreach (MonoBehaviour mono in lateUpdateScripts)
            {
                mono.LateUpdate();
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
            Assembly gameAssembly = Assembly.Load("OpenGL.Game");
            Assembly customAssembly = Assembly.Load("OpenGL.Custom");

            var types = gameAssembly.GetTypes().Concat(customAssembly.GetTypes()).Where(t => t.IsSubclassOf(typeof(MonoBehaviour)));

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


