using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Textures;
using osu.Framework.Logging;
using osuTK;
using TestTest123.Game.Material;

namespace TestTest123.Game
{
    public class MeshDrawNode : ThreeDimensionalDrawNode
    {


        public Texture Texture { get; private set; }


        public Mesh Mesh;
        private UniformMaterial uniformMaterial;

        public MeshDrawNode(Mesh mesh, ModelDrawNode parent) : base(parent)
        {
            Name = mesh.Name;
            Mesh = mesh;
            

            Assimp.Material material = parent.Model.Materials[mesh.MaterialIndex];
            uniformMaterial = new UniformMaterial(material.ColorDiffuse);
            if (material.HasTextureDiffuse)
            {
                Texture = TextureStore.Get(material.TextureDiffuse.FilePath);
            }
        }


        public override void Draw(IRenderer renderer)
        {

            renderer.PushProjectionMatrix(Parent.LocalMatrix.Value * renderer.ProjectionMatrix);

            Scene.Shaderer?.BindUniform(uniformMaterial);
                Texture?.Bind();
                Mesh.Draw();
            renderer.PopProjectionMatrix();

        }
    }
}
