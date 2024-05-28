using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Assimp;
using HidSharp.Reports;
using NuGet.Protocol;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Extensions.EnumExtensions;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Pooling;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Rendering.Vertices;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Shaders.Types;
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

        public List<Material> Materials = new List<Material>();
        public List<MeshDrawable> Meshes = new List<MeshDrawable>();
        public readonly string FilePath;
        public ThreeDimensionalStageDrawable Stage;

        public override DrawColourInfo DrawColourInfo => new DrawColourInfo(Colour.AverageColour);
        public ModelDrawable(string filepath, ThreeDimensionalStageDrawable stage)
        {

            Stage = stage;
            FilePath = filepath;
        }

        protected override bool OnInvalidate(Invalidation invalidation, InvalidationSource source)
        {
            if (invalidation.HasFlagFast(Invalidation.Colour))
            {
                foreach (MeshDrawable mesh in Meshes)
                {

                    mesh.Colour = Colour;
                }
            }

            return base.OnInvalidate(invalidation, source);
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textureStore)
        {
            
            AssimpContext importer = new AssimpContext();

            if (FilePath != null)
            {
                Scene sceneInfo = importer.ImportFile(FilePath, PostProcessSteps.Triangulate | PostProcessSteps.FlipUVs);
                loadMaterials(sceneInfo);
            }
        }

        private void loadMeshes(Scene sceneInfo)
        {
            if (!sceneInfo.HasMeshes) { return; }


            foreach (Mesh mesh in sceneInfo.Meshes)
            {
                generateNewMesh(mesh);
            }
       }

        private void generateNewMesh(Mesh mesh)
        {
            MeshDrawable meshDrawable = new MeshDrawable(this, mesh);
            meshDrawable.Colour = Colour;
            Meshes.Add(meshDrawable);
        }


        private void loadMaterials(Scene sceneInfo)
        {

            foreach (Assimp.Material assimp in sceneInfo.Materials)
            {
                Material material = Stage.GetMaterial(GetType(), assimp);
                Materials.Add(material);
            }


            loadMeshes(sceneInfo);
        }
    }
}
