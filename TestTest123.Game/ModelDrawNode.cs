
using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Extensions.EnumExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Textures;
using osu.Framework.Layout;
using osuTK;
using osuTK.Graphics.OpenGL;
using TestTest123.Game.Material;
namespace TestTest123.Game
{
    public abstract class ModelDrawNode : ThreeDimensionalDrawNode
    {


        public Model Model { get; private set; }
        public List<MeshDrawNode> Meshes { get; private set; } = [];

        

        public ModelDrawNode(SceneNode scene) : base(scene)
        {
            Model = LoadModel(Scene.Renderer);
            Name.Value = Model.Filepath;

            loadMeshes();

            

        }

        protected abstract Model LoadModel(IRenderer renderer);


        private void loadMeshes()
        {
            foreach (Mesh mesh in Model.Meshes)
            {
                MeshDrawNode test = new MeshDrawNode(mesh, this);
                Meshes.Add(test);
            }
        }

    }
}
