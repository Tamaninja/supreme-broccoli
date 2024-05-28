using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Logging;
using osuTK;
using TestTest123.Game.Vertices;

namespace TestTest123.Game
{
    public class MeshDrawNode : DrawNode
    {

        protected new MeshDrawable Source => (MeshDrawable)base.Source;
        private Matrix4 vpMatrix = Matrix4.Identity;
        private Matrix4 modelMatrix = Matrix4.Identity;
        public MeshDrawNode(MeshDrawable source) : base(source)
        {

        }



        public override void ApplyState()
        {
            base.ApplyState();

            modelMatrix = Source.LocalMatrix * Source.Model.LocalMatrix;
            vpMatrix = Source.Model.Stage.Camera.VPMatrix;
        }

        protected override void Draw(IRenderer renderer)
        {
            Source.TextureShader.Bind();
            renderer.PushDepthInfo(DepthInfo.Default);
            renderer.PushProjectionMatrix(modelMatrix * vpMatrix);

                Source.Mesh.Draw(renderer);

            renderer.PopProjectionMatrix();
            renderer.PopDepthInfo();
            Source.TextureShader.Unbind();


        }
    }
}
