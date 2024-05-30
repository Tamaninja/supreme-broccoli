using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Rendering.Vertices;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Logging;
using osuTK;
using osuTK.Graphics;
using TestTest123.Game.Vertices;

namespace TestTest123.Game
{
    public partial class MeshDrawable : ThreeDimensionalDrawable
    {
        public ModelDrawable Model { get; set; }


        public Mesh Mesh;
        public MaterialDrawable Material;

        public MeshDrawable(ModelDrawable parent, Mesh mesh){

            Mesh = mesh;
            Model = parent;
            Material = parent.Materials[mesh.MaterialIndex];
            Colour = Material.Colour;

            Material.Add(CreateProxy());
        }


        [BackgroundDependencyLoader]
        private void load(ShaderManager shaders, IRenderer renderer, TextureStore textureStore)
        {

        }
        protected override DrawNode CreateDrawNode()
        {
           
            return (new MeshDrawNode(this));

        }
    }
}
