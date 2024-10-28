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
        private IUniformBuffer<UniformMaterial> uniformBuffer;
        public Texture Texture { get; private set; }




        public Mesh Mesh;
        public MeshDrawNode(Mesh mesh, ModelDrawNode parent) : base(parent)
        {
            Name = mesh.Name;
            Mesh = mesh;


            Assimp.Material material = parent.Model.Materials[mesh.MaterialIndex];
            if (material.HasTextureDiffuse)
            {
                Texture = TextureStore.Get(parent.Model.Materials[mesh.MaterialIndex].TextureDiffuse.FilePath);
            }
        }


        public override void Draw(IRenderer renderer)
        {
            /*            uniformBuffer ??= renderer.CreateUniformBuffer<UniformMaterial>();
                        uniformBuffer.Data = material;

                        Source.Material.TextureShader.BindUniformBlock("u_Colour", uniformBuffer);*/
            renderer.PushProjectionMatrix(Parent.LocalMatrix.Value * renderer.ProjectionMatrix);

                Texture?.Bind();
                Mesh.Draw();
            renderer.PopProjectionMatrix();

        }
    }
}
