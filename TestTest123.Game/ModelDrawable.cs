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

        
        public Model Model { get; set; }
        public readonly string FilePath;
        public ThreeDimensionalStageDrawable Stage;

        public ModelDrawable(string filepath, ThreeDimensionalStageDrawable stage)
        {
            Stage = stage;
            FilePath = filepath;
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textureStore)
        {
            
            AssimpContext importer = new AssimpContext();

            if (FilePath != null)
            {
                Scene sceneInfo = importer.ImportFile(FilePath, PostProcessSteps.Triangulate | PostProcessSteps.FlipUVs);
                Model = new Model(sceneInfo);

                foreach (Mesh mesh in Model.Meshes)
                {
                    AddInternal(new MeshDrawable(mesh, this));
                }
            }
        }
    }
}
