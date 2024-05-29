using System.Collections.Generic;
using Assimp;
using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Textures;
using osu.Framework.Layout;
using osu.Framework.Logging;
using osu.Framework.Testing;
using osuTK;
using osuTK.Graphics;
using TestTest123.Game.Vertices;
namespace TestTest123.Game
{
    public partial class ModelDrawable : ThreeDimensionalDrawable
    {


        public Model Model { get; private set; }
        public ThreeDimensionalStageDrawable Stage;
        public List<MaterialDrawable> Materials { get; set; } = [];

        public ModelDrawable(Model model, ThreeDimensionalStageDrawable stage)
        {
            Stage = stage;
            Model = model;
        }

        [BackgroundDependencyLoader]
        private void load(IRenderer renderer, TextureStore textureStore)
        {
            loadMaterials();


        }

        public void AddInternal(MaterialDrawable material)
        {
            
            base.AddInternal(material);
            Materials.Add(material);
        }
        private void loadMaterials()
        {
            for (int i = 0; i < Model.Materials.Count; i++)
            {
                AddInternal(new MaterialDrawable(Model.Materials[i]));
            }

            loadMeshes();
        }
        private void loadMeshes()
        {
            foreach (Mesh mesh in Model.Meshes)
            {
                Materials[mesh.MaterialIndex].Add(new MeshDrawable(this, mesh));
            }
        }

    }
}
