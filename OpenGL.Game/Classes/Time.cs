using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL.Game
{
    public class Time : MonoBehaviour
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

        public override void Awake()
        {
            applicationStarted = DateTime.Now;
            previousFrame = DateTime.Now;
        }

        public override void Update()
        {
            currentFrame = DateTime.Now;
            deltaTime = (currentFrame - previousFrame).TotalSeconds;
            previousFrame = currentFrame;
        }
    }
}
