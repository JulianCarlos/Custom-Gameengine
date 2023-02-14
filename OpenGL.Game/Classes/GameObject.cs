using System.Collections.Generic;
using OpenTK;
using OpenGL.Game.AbstractClasses;
using OpenGL.Game.Helper;
using OpenGL.Game.Library;
using OpenGL.Game.Meshes;
using Assimp;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System;
using OpenTK.Graphics.OpenGL;
using TextureWrapMode = Assimp.TextureWrapMode;
using System.Linq;

namespace OpenGL.Game
{
    public class GameObject : Object
    {
        public bool Active { get; private set; } = true;

        public Transform transform { get; set; }
        public MeshRenderer meshRenderer { get; set; }
        public Material material;

        private List<Component> components = new List<Component>();

        public GameObject()
        {
            transform = AddComponent<Transform>();
            material = MaterialLibrary.defaultMaterial;
        }

        public void StartAllComponents()
        {
            foreach (var component in components)
            {
                component.Awake();
            }
        }

        public void UpdateAllComponents()
        {
            foreach (var component in components)
            {
                component.Update();
                meshRenderer.Update();
            }
        }

        public void SetActive(bool active)
        {
            this.Active = active;
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

            gameObject.Name = "Cube";
            gameObject.meshRenderer = new MeshRenderer(MeshLibrary.DefaultCubeMesh, gameObject);

            Scene.AddObject(gameObject.transform);
            return gameObject;
        }

        //TODO
        public static GameObject LoadModel(string fileName)
        {
            GameObject gameObject = new GameObject();

            // Load the model using Assimp
            //var importer = new AssimpContext();
            //var model = importer.ImportFile(fileName, PostProcessSteps.Triangulate | PostProcessSteps.FlipUVs);


            // Get the first mesh in the model
            //var mesh = model.Meshes[0];

            //Console.WriteLine(model.MeshCount);
            //Console.WriteLine(model.TextureCount);
            //Vector3[] Vertices = new Vector3[mesh.VertexCount];
            //for (int i = 0; i < mesh.Vertices.Count; i++)
            //{
            //    Vertices[i] = new Vector3(mesh.Vertices[i].X, mesh.Vertices[i].Y, mesh.Vertices[i].Z);
            //}
            //
            //Vector3[] Normals = new Vector3[mesh.VertexCount];
            //for (int i = 0; i < mesh.Normals.Count; i++)
            //{
            //    Normals[i] = new Vector3(mesh.Normals[i].X, mesh.Normals[i].Y, mesh.Normals[i].Z);
            //}
            //
            //Vector2[] Uvs = new Vector2[mesh.VertexCount];
            //for (int i = 0; i < mesh.TextureCoordinateChannelCount; i++)
            //{
            //    Uvs[i] = new Vector2(mesh.TextureCoordinateChannels[0][i].X, mesh.TextureCoordinateChannels[0][i].Y);
            //}
            //
            //int[] tempIndices = mesh.GetIndices();
            //uint[] Indices = new uint[mesh.GetIndices().Length];
            //for (int i = 0; i < tempIndices.Length; i++)
            //{
            //    Indices[i] = (uint)tempIndices[i];
            //}
            //
            //Mesh stupidMesh = new Mesh(Vertices, Uvs, Normals, Indices);
            //gameObject.meshRenderer = new MeshRenderer(stupidMesh, gameObject);
            //
            return gameObject;
        }

        public T AddComponent<T>() where T: Component, new()
        {
            T component = new T();
            component.gameObject = this;
            this.components.Add(component);
            component.Awake();
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
