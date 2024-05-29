using HidSharp.Reports;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Textures;
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
        private Texture texture;
        private Mesh mesh;
        private MaterialDrawable material;
        public MeshDrawNode(MeshDrawable source) : base(source)
        {
            mesh = source.Mesh;
        }



        public override void ApplyState()
        {
            base.ApplyState();
            mesh = Source.Mesh;
            material = Source.Material;
            
            modelMatrix = Source.LocalMatrix * Source.Model.LocalMatrix;
            vpMatrix = Source.Model.Stage.Camera.VPMatrix;
        }

        protected override void Draw(IRenderer renderer)
        {

            Source.Material.TextureShader.Bind();
            Source.Material.Texture?.Bind();
            renderer.PushDepthInfo(DepthInfo.Default);
            renderer.PushProjectionMatrix(modelMatrix * vpMatrix);


            mesh.DrawVBO(renderer);

            renderer.PopDepthInfo();
            renderer.PopProjectionMatrix();

            Source.Material.TextureShader.Unbind();
        }
    }
}
