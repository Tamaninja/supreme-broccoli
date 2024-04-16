using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osuTK;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Primitives;
using System.Collections.Generic;
using osu.Framework.Logging;
using Vortice;
using osuTK.Graphics.OpenGL;
using System.Runtime.CompilerServices;
using osuTK.Graphics;
using osu.Framework.Extensions.Color4Extensions;



namespace TestTest123.Game
{
    public partial class Box3D : Sprite
    {

        private List<Quad> quads;
        public Vector3[] Vertices;
        private Camera camera;
        public Box3D(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4, Vector3 v5, Vector3 v6, Vector3 v7, Vector3 v8, Camera camera)
        {
            Vertices = [ v1, v2, v3, v4, v5, v6, v7, v8 ];
            this.camera = camera;
            RelativeSizeAxes = Axes.Both;
            this.Colour = Color4.AliceBlue.Opacity(0.5f);
        }

        public Vector3[] GetVertices()
        {
            return (Vertices);
        }

        protected override Quad ComputeScreenSpaceDrawQuad()
        {
            return ToScreenSpace(DrawRectangle);
        }

        public Quad ToScreenSpace(Quad quad)
        {
            return (quad * DrawInfo.Matrix);
        }

        

        [BackgroundDependencyLoader]
        private void load(IRenderer renderer)
        {

            base.Texture = renderer.WhitePixel;
        }

        protected override DrawNode CreateDrawNode() => new BoxNode(this);


        public class BoxNode : SpriteDrawNode
        {
            protected new Box3D Source => (Box3D)base.Source;
            private float width;
            private float height;
            private Camera camera;
            private Vector3[] vertices1;
            public BoxNode(Box3D source)
                : base(source)
            {
                width = Source.DrawWidth;
                height = Source.DrawHeight;
                camera = Source.camera;
                vertices1 = Source.GetVertices();
            }

            private void render(IRenderer renderer)
            {
                Vector3[] vertices = new Vector3[8];
                Matrix4 matrix = Matrix4.CreatePerspectiveFieldOfView(2, 16 / 9, 1, 5000);
                for (int i = 0; i < vertices.Length; i++)
                {
                    vertices[i] = Vector3.Project(camera.XYZ3D - vertices1[i], 1, 1, width, height, 1, 5000, matrix);
                }

                Quad[] faces =
                [
                    // Bottom face
                    new Quad(vertices[0].Xy, vertices[3].Xy, vertices[1].Xy, vertices[2].Xy),
                    // Top face
                    new Quad(vertices[5].Xy, vertices[4].Xy, vertices[6].Xy, vertices[7].Xy),
                    // Left face
                    new Quad(vertices[0].Xy, vertices[4].Xy, vertices[3].Xy, vertices[7].Xy),
                    // Right face
                    new Quad(vertices[1].Xy, vertices[5].Xy, vertices[2].Xy, vertices[6].Xy),
                    // Front face
                    new Quad(vertices[0].Xy, vertices[1].Xy, vertices[4].Xy, vertices[5].Xy),
                    // Back face
                    new Quad(vertices[2].Xy, vertices[3].Xy, vertices[6].Xy, vertices[7].Xy),
                ];

                for (int i = 0; i < 6; i++) 
                {

                    renderer.DrawQuad(Texture, Source.ToScreenSpace(faces[i]), DrawColourInfo.Colour, null, null,
                        new Vector2(InflationAmount.X / DrawRectangle.Width, InflationAmount.Y / DrawRectangle.Height));
                }
            }

            protected override void Blit(IRenderer renderer)
            {
                if (DrawRectangle.Width == 0 || DrawRectangle.Height == 0)
                    return;
                render(renderer);

            }



            protected override void BlitOpaqueInterior(IRenderer renderer)
            {
                if (DrawRectangle.Width == 0 || DrawRectangle.Height == 0)
                    return;

                if (renderer.IsMaskingActive)
                    render(renderer);
                else
                    render(renderer);
            }


        }
    }
}
