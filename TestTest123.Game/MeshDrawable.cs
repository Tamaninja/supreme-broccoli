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
using TestTest123.Game.Material;
using TestTest123.Game.Vertices;

namespace TestTest123.Game
{
    public partial class MeshDrawable : ThreeDimensionalDrawable
    {
        public ModelDrawable Model { get; set; }
        public MaterialDrawable Material;



        public readonly Mesh Mesh;
        public override Matrix4 Matrix => Model.Matrix * base.Matrix;


        public MeshDrawable(ModelDrawable parent, Mesh mesh) {
            parent.LocalMatrix.BindValueChanged((t) => Invalidate(Invalidation.DrawNode, osu.Framework.Layout.InvalidationSource.Parent));
            CameraViewProjection.BindTo(parent.CameraViewProjection);

            Name = mesh.Name;
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
