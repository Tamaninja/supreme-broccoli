using HidSharp.Reports;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Textures;
using osu.Framework.Logging;
using osuTK;
using osuTK.Graphics;
using TestTest123.Game.Material;
using TestTest123.Game.Vertices;
using Vortice;

namespace TestTest123.Game
{
    public partial class MeshDrawable
    {
        protected class MeshDrawNode : CompositeDrawableDrawNode
        {
            private IUniformBuffer<UniformMaterial> uniformBuffer;
            private UniformMaterial material;
            
            protected new MeshDrawable Source => (MeshDrawable)base.Source;
            private Matrix4 localMatrix = Matrix4.Identity;
            private Matrix4 vpMatrix = Matrix4.Identity;
            private Matrix4 premultiplied = Matrix4.Identity;

            private Mesh mesh;
            public MeshDrawNode(MeshDrawable source) : base(source)
            {

                mesh = source.Mesh;
                material = new UniformMaterial { Colour = source.Colour.TopLeft.ToVector() };
            }



            public override void ApplyState()
            {
                base.ApplyState();

                localMatrix = Source.Matrix;
                vpMatrix = Source.CameraViewProjection.Value;
                premultiplied = localMatrix * vpMatrix;
                material.Colour = Source.Colour.TopLeft.ToVector();
            }

            protected override void Draw(IRenderer renderer)
            {
                uniformBuffer ??= renderer.CreateUniformBuffer<UniformMaterial>();
                uniformBuffer.Data = material;

                Source.Material.TextureShader.BindUniformBlock("u_Colour", uniformBuffer);


                renderer.PushDepthInfo(DepthInfo.Default);
                renderer.PushProjectionMatrix(premultiplied);

                mesh.Draw(renderer);

                renderer.PopProjectionMatrix();
                renderer.PopDepthInfo();


            }
        }
    }

}
