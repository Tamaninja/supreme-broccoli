using HidSharp.Reports;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Textures;
using osu.Framework.Logging;
using osuTK;
using TestTest123.Game.Vertices;

namespace TestTest123.Game
{
    public partial class MeshDrawable
    {
        protected class MeshDrawNode : CompositeDrawableDrawNode
        {

            protected new MeshDrawable Source => (MeshDrawable)base.Source;
            private Matrix4 localMatrix = Matrix4.Identity;
            private Matrix4 vpMatrix = Matrix4.Identity;
            private Mesh mesh;
            public MeshDrawNode(MeshDrawable source) : base(source)
            {
                mesh = source.Mesh;
            }



            public override void ApplyState()
            {
                base.ApplyState();
                mesh = Source.Mesh;

                localMatrix = Source.Model.GetMatrix();
                vpMatrix = Source.Model.Stage.Camera.VPMatrix;
            }

            protected override void Draw(IRenderer renderer)
            {

                renderer.PushDepthInfo(DepthInfo.Default);
                renderer.PushProjectionMatrix(localMatrix * vpMatrix);

                mesh.DrawVBO(renderer);

                renderer.PopProjectionMatrix();
                renderer.PopDepthInfo();


            }
        }
    }

}
