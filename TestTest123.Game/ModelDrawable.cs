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
using SharpGen.Runtime.Win32;
using TestTest123.Game.Vertices;
namespace TestTest123.Game
{
    public partial class ModelDrawable : ThreeDimensionalDrawable
    {


        public Model Model { get; private set; }
        public ThreeDimensionalStageDrawable Stage;
        public List<MaterialDrawable> Materials { get; set; } = [];
        public List<MeshDrawable> Meshes { get; set; } = [];


        public ModelDrawable(Model model, ThreeDimensionalStageDrawable stage)
        {
            Logger.LogPrint("13");
            Stage = stage;
            Model = model;
        }

        [BackgroundDependencyLoader]
        private void load(IRenderer renderer, TextureStore textureStore)
        {
            loadMaterials();
            loadMeshes();


        }

        private void loadMaterials()
        {
            if (!Stage.Materials.TryGetValue(Model, out var materials))
            {
                materials = new List<MaterialDrawable>();
                for (int i = 0; i < Model.Materials.Count; i++)
                {
                    materials.Add(new MaterialDrawable(Model.Materials[i]));
                    
                }
                
                Stage.Materials.TryAdd(Model, materials);
                Stage.AddRange(materials);
            }

            Materials = materials;
        }

        public void AddInternal(MeshDrawable mesh)
        {
            base.AddInternal(mesh);
            Meshes.Add(mesh);
        }
        private void loadMeshes()
        {
            foreach (Mesh mesh in Model.Meshes)
            {
                AddInternal(new MeshDrawable(this, mesh));
            }
        }
        
    }
}
