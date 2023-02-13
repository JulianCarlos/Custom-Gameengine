using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL.Game
{
    public static class Time
    {
        public static float DeltaTime
        {
            get { return (float)deltaTime; }
        }

        public static float time
        {
            get { return (float)(DateTime.Now - applicationStarted).TotalSeconds; }
        }

        private static double deltaTime;
        private static DateTime currentFrame;
        private static DateTime previousFrame;
        private static DateTime applicationStarted;

        public static void StartTime()
        {
            applicationStarted = DateTime.Now;
            previousFrame = DateTime.Now;
        }

        public static void UpdateTime()
        {
            currentFrame = DateTime.Now;
            deltaTime = (currentFrame - previousFrame).TotalSeconds;
            previousFrame = currentFrame;
        }
    }
}
