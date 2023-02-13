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
using ConsoleApp1;
using OpenGL.Custom;
using OpenGL.Game.Library;
using OpenGL.Game.Meshes;

namespace OpenGL.Loop
{
    public class Program : GameWindow
    {
        public static int width = 1600;
        public static int height = 800;

        private Skybox skybox;

        private GameObject playerBody;
        private GameObject mapGeneratorGameObject;

        public Program() : base(width, height, GraphicsMode.Default, "My poor (not so poor anymore) Game")
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

            ShaderLibrary.GenerateShaders();
            MeshLibrary.GenerateMeshes();

            playerBody = GameObject.CreatePrimitives(GameObject.PrimitiveType.cube);
            playerBody.SetActive(false);
            playerBody.AddComponent<PlayerController>();

            mapGeneratorGameObject = GameObject.CreatePrimitives(GameObject.PrimitiveType.cube);
            mapGeneratorGameObject.SetActive(false);
            mapGeneratorGameObject.AddComponent<MapGenerator>();

            CursorVisible = false;
            CursorGrabbed = true;

            skybox = new Skybox();

            Scene.StartScene();
            Time.StartTime();
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

            Scene.UpdateScene();

            GL.DepthFunc(DepthFunction.Lequal);
            if(Scene.Cameras.Count > 0) skybox.DrawSkybox(Scene.Cameras[0]);
            GL.DepthFunc(DepthFunction.Less);

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            var keyboard = Keyboard.GetState();
            if (keyboard[Key.Escape])
                Exit();

            Input.UpdateInput();
            Time.UpdateTime();

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
    }
}


