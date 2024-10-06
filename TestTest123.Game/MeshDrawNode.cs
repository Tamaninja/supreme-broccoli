using osu.Framework.Graphics.Rendering;
using osu.Framework.Logging;
using osuTK;
using TestTest123.Game.Material;

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

                    mesh.Draw();

                renderer.PopProjectionMatrix();
                renderer.PopDepthInfo();


            }

            protected override void Dispose(bool isDisposing)
            {
                mesh.Dispose();
                base.Dispose(isDisposing);
            }
        }
    }

}
