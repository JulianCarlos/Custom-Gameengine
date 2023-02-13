using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL.Game.AbstractClasses
{
    public abstract class Component : Object
    {
        public GameObject gameObject;

        public virtual void Awake()
        {

        }

        public virtual void Update()
        {

        }
    }
}
    