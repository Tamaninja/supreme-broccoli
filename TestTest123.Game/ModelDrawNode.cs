using System.Collections.Generic;
using Assimp;
using osu.Framework.Graphics.Rendering;
namespace TestTest123.Game
{
    public abstract class ModelDrawNode : ThreeDimensionalDrawNode
    {


        public Model Model { get; private set; }
        public MeshDrawNode[] Meshes { get; private set; }

        

        public ModelDrawNode(SceneNode scene) : base(scene)
        {
            Model = LoadModel(Scene.Renderer);
            Name.Value = Model.Filepath;

            loadMeshes();


        }
        public NodeInstance CreateInstance()
        {
            NodeInstance instance = new NodeInstance(this);
            foreach (var mesh in Meshes)
            {
                mesh.Instances.Add(instance);
            }


            return instance;
        }
        protected abstract Model LoadModel(IRenderer renderer);


        private void loadMeshes()
        {
            Meshes = new MeshDrawNode[Model.Meshes.Count];

            for (int i = 0; i < Model.Meshes.Count; i++)
            {
                Meshes[i] = new MeshDrawNode(Model.Meshes[i], this);
            }
        }

    }
}
