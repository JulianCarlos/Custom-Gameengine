using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OpenGL.Game;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Assimp;
using OpenGL.Game.AbstractClasses;

namespace OpenGL.Game
{
    public class GameObject
    {
        public bool Active { get; private set; } = true;

        public Transform transform { get; set; }
        public MeshRenderer meshRenderer { get; set; }
        public Material material;

        public GameObject Parent;
        public List<GameObject> Children = new List<GameObject>();

        private List<Component> components = new List<Component>();

        public GameObject()
        {
            transform = AddComponent<Transform>();

            material = Material.defaultMaterial;
        }

        public void SetActive(bool active)
        {
            this.Active = active;
        }

        public void AddChild(GameObject child)
        {
            Children.Add(child);
            child.Parent = this;
        }

        public static GameObject CreatePrimitives(PrimitiveType type)
        {
            switch (type)
            {
                default:
                case PrimitiveType.cube:
                    return CreateCube();

                case PrimitiveType.triangle:
                    return null;

                case PrimitiveType.quad:
                    return null;

                case PrimitiveType.sphere:
                    return null;
            }
        }

        private static GameObject CreateCube()
        {
            GameObject gameObject = new GameObject();

            Vector3[] vertices = new Vector3[]
            {
                //Front
                new Vector3(-0.5f, -0.5f, 0.5f),
                new Vector3(0.5f, -0.5f, 0.5f),
                new Vector3(-0.5f, 0.5f, 0.5f),
                new Vector3(0.5f, 0.5f, 0.5f),
                
                //Back
                new Vector3(-0.5f, -0.5f, -0.5f),
                new Vector3(0.5f, -0.5f, -0.5f),
                new Vector3(-0.5f, 0.5f, -0.5f),
                new Vector3(0.5f, 0.5f, -0.5f),

                //Right
                new Vector3(0.5f, -0.5f, -0.5f),
                new Vector3(0.5f, -0.5f, 0.5f),
                new Vector3(0.5f, 0.5f, -0.5f),
                new Vector3(0.5f, 0.5f, 0.5f),

                //Left
                new Vector3(-0.5f, -0.5f, -0.5f),
                new Vector3(-0.5f, -0.5f, 0.5f),
                new Vector3(-0.5f, 0.5f, -0.5f),
                new Vector3(-0.5f, 0.5f, 0.5f),

                //Top
                new Vector3(-0.5f, 0.5f, -0.5f),
                new Vector3(0.5f, 0.5f, -0.5f),
                new Vector3(-0.5f, 0.5f, 0.5f),
                new Vector3(0.5f, 0.5f, 0.5f),

                //Bottom
                new Vector3(-0.5f, -0.5f, -0.5f),
                new Vector3(0.5f, -0.5f, -0.5f),
                new Vector3(-0.5f, -0.5f, 0.5f),
                new Vector3(0.5f, -0.5f, 0.5f)
            };

            Vector2[] uvs = new Vector2[] 
            {
                new Vector2(0f,0f),
                new Vector2(1f,0f),
                new Vector2(0f,1f),
                new Vector2(1f,1f),

                new Vector2(0f,0f),
                new Vector2(1f,0f),
                new Vector2(0f,1f),
                new Vector2(1f,1f),

                new Vector2(0f,0f),
                new Vector2(1f,0f),
                new Vector2(0f,1f),
                new Vector2(1f,1f),

                new Vector2(0f,0f),
                new Vector2(1f,0f),
                new Vector2(0f,1f),
                new Vector2(1f,1f),

                new Vector2(0f,0f),
                new Vector2(1f,0f),
                new Vector2(0f,1f),
                new Vector2(1f,1f),

                new Vector2(0f,0f),
                new Vector2(1f,0f),
                new Vector2(0f,1f),
                new Vector2(1f,1f)
            };

            Vector3[] normals = new Vector3[] 
            {
                new Vector3(0.0f, 0.0f, 1.0f),
                new Vector3(0.0f, 0.0f, 1.0f),
                new Vector3(0.0f, 0.0f, 1.0f),
                new Vector3(0.0f, 0.0f, 1.0f),
                new Vector3(0.0f, 0.0f, -1.0f),
                new Vector3(0.0f, 0.0f, -1.0f),
                new Vector3(0.0f, 0.0f, -1.0f),
                new Vector3(0.0f, 0.0f, -1.0f),
                new Vector3(0.0f, -1.0f, 0.0f),
                new Vector3(0.0f, -1.0f, 0.0f),
                new Vector3(0.0f, -1.0f, 0.0f),
                new Vector3(0.0f, -1.0f, 0.0f),
                new Vector3(0.0f, 1.0f, 0.0f),
                new Vector3(0.0f, 1.0f, 0.0f),
                new Vector3(0.0f, 1.0f, 0.0f),
                new Vector3(0.0f, 1.0f, 0.0f),
            };

            uint[] indices = new uint[] 
            {
                  // Front Face
                  0, 1, 2, // First Triangle
                  1, 3, 2, // Second Triangle

                  // Back Face
                  2 + 4, 1 + 4, 0 + 4, // First Triangle
                  3 + 4, 1 + 4, 2 + 4, // Second Triangle

                  // Right Face
                  2 + 8, 1 + 8, 0 + 8, // First Triangle
                  3 + 8, 1 + 8, 2 + 8, // Second Triangle

                  // Left Face
                  0 + 12, 1 + 12, 2 + 12, // First Triangle
                  1 + 12, 3 + 12, 2 + 12, // Second Triangle

                  // Top Face
                  2 + 16, 1 + 16, 0 + 16, // First Triangle
                  3 + 16, 1 + 16, 2 + 16, // Second Triangle

                  // Bottom Face
                  0 + 20, 1 + 20, 2 + 20, // First Triangle
                  1 + 20, 3 + 20, 2 + 20, // Second Triangle
            };

            Mesh mesh = new Mesh(vertices, uvs, normals, indices);
            MeshRenderer meshRenderer = new MeshRenderer(mesh, gameObject);
            gameObject.meshRenderer = meshRenderer;
            return gameObject;
        }

        public T AddComponent<T>() where T: Component, new()
        {
            T component = new T();
            component.gameObject = this;
            this.components.Add(component);
            return component;
        }

        public Component GetComponent<T>()
        {
            foreach (var component in components)
            {
                if(component is T)
                {
                    return component;
                }
            }
            return null;
        }

        public enum PrimitiveType
        {
            cube, 
            triangle,
            quad,
            sphere
        }
    }
}
