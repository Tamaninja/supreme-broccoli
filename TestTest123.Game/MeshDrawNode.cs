using Assimp;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Textures;
using osu.Framework.Logging;
using osuTK;
using TestTest123.Game.Material;
using Mesh = osu.Framework.Graphics.Rendering.Mesh;

namespace TestTest123.Game
{
    public class MeshDrawNode : ThreeDimensionalDrawNode
    {


        public Texture Texture { get; private set; }


        public Mesh Mesh;
        public ModelDrawNode Parent;
        public Assimp.Material Material { get; private set; }

        private UniformMaterial uniformMaterial => new UniformMaterial(Parent.Colour * Colour);

        public MeshDrawNode(Mesh mesh, ModelDrawNode parent) : base(parent)
        {
            Name.Value = mesh.Name;
            Mesh = mesh;
            Parent = parent;
            Material = parent.Model.Materials[mesh.MaterialIndex];
            Colour = Material.ColorDiffuse.ToColour4();


            if (Material.HasTextureDiffuse)
            {
                
                Texture = Scene.TextureStore.Get(Material.TextureDiffuse.FilePath);
            }
            Scene.AssignShaderer(this);

        }


        public override void Draw(IRenderer renderer)
        {

            Texture ??= renderer.WhitePixel;
            renderer.PushProjectionMatrix(Parent.LocalMatrix.Value * renderer.ProjectionMatrix);

            Scene.CurrentShaderer?.BindUniform(uniformMaterial);
                Texture?.Bind();
                Mesh.Draw();
            renderer.PopProjectionMatrix();

        }
    }
}
