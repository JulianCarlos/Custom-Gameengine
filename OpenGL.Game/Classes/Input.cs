using OpenTK;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL.Game.Classes
{
    public class Input
    {
        public static int HorizontalInput { get { return GetHorizontalInput(); } }
        public static int VerticalInput { get { return GetVerticalInput(); } }

        public static Vector2 mouseDelta;

        private static Vector2 currentMousePosition;
        private static Vector2 previousMousePosition;

        private static KeyboardState keyboardState = new KeyboardState();
        private static MouseState mouseState;

        private static Vector2 CalculateMouseDelta()
        {
            mouseState = Mouse.GetState();
            int x = mouseState.X * -1;
            int y = mouseState.Y * -1;
            currentMousePosition = new Vector2(x, y);

            mouseDelta = currentMousePosition - previousMousePosition;
            previousMousePosition = currentMousePosition;

            return mouseDelta / 100;
        }

        public static void UpdateInput()
        {
            mouseDelta = CalculateMouseDelta();
        }
        
        private static int GetHorizontalInput()
        {
            return keyboardState.IsKeyDown(Key.D) ? 1 : (keyboardState.IsKeyDown(Key.A) ? -1 : 0);
        }

        private static int GetVerticalInput()
        {
            keyboardState = Keyboard.GetState();

            return keyboardState.IsKeyDown(Key.W) ? 1 : (keyboardState.IsKeyDown(Key.S) ? -1 : 0);
        }
    }
}
