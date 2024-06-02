
using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Extensions.EnumExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Textures;
using osu.Framework.Layout;
using osu.Framework.Logging;
using osu.Framework.Testing;
using osuTK;
using osuTK.Graphics;
using SharpGen.Runtime.Win32;
using TestTest123.Game.Material;
using TestTest123.Game.Vertices;
namespace TestTest123.Game
{
    public partial class ModelDrawable : ThreeDimensionalDrawable
    {


        public Model Model { get; private set; }
        public ThreeDimensionalStageDrawable Stage;
        public Container<MaterialDrawable> Materials { get; private set; } = [];
        public List<MeshDrawable> Meshes { get; private set; } = [];


        public ModelDrawable(Model model, ThreeDimensionalStageDrawable stage)
        {
            Stage = stage;
            Model = model;

            
        }

        [BackgroundDependencyLoader]
        private void load(MaterialStore materialStore)
        {
            Materials = materialStore.GetMaterials(Model);

            loadMeshes();
        }

        
        protected override bool OnInvalidate(Invalidation invalidation, InvalidationSource source)
        {

            if (invalidation.HasFlagFast(Invalidation.Colour))
            {
                for (int i = 0;i < Meshes.Count ;i++)
                {
                    Meshes[i].Colour = Meshes[i].Material.Colour.TopLeft * Colour.TopLeft;
                }
            }

            return base.OnInvalidate(invalidation, source);
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
                MeshDrawable test = new MeshDrawable(this, mesh);
                AddInternal(test);
            }
        }
        
    }
}
