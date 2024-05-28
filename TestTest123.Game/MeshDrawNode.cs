using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Logging;
using osuTK;
using TestTest123.Game.Vertices;

namespace TestTest123.Game
{
    public partial class MeshDrawable
    {
        protected class MeshDrawNode<T> : DrawNode
            where T : unmanaged, IMeshVertex<T>
        {

            private IVertexBatch<T> vertexBatch;
            protected new MeshDrawable Source => (MeshDrawable)base.Source;
            private Matrix4 vpMatrix = Matrix4.Identity;
            private Matrix4 modelMatrix = Matrix4.Identity;
            public MeshDrawNode(MeshDrawable source) : base(source)
            {
                
            }



            public override void ApplyState()
            {
                base.ApplyState();

                modelMatrix = Source.Model.LocalMatrix;
                vpMatrix = Source.Model.Stage.Camera.VPMatrix;
            }

            protected override void Draw(IRenderer renderer)
            {
                renderer.PushDepthInfo(DepthInfo.Default);
                renderer.PushProjectionMatrix(modelMatrix * vpMatrix);

                vertexBatch ??= renderer.CreateLinearBatch<T>(Source.Indices.Length * 3, 3, PrimitiveTopology.Triangles);
                renderer.BindTexture(renderer.WhitePixel);
                for (int i = 0; i < Source.Indices.Length; i++)
                {
                    vertexBatch.AddAction(T.FromMesh(Source, Source.Indices[i]));
                }

                renderer.PopProjectionMatrix();
                renderer.PopDepthInfo();
            }
        }
    }
}
