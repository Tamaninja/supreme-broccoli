using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osuTK;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Lines;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Textures;

namespace TestTest123.Game
{
    public partial class QuadSprite : Sprite
    {

        private Quad quadToDraw;
        public new RectangleF DrawRectangle;

        public QuadSprite(Vector2 topLeft, Vector2 topRight, Vector2 bottomLeft, Vector2 bottomRight)
        {

            quadToDraw = new Quad(topLeft, topRight, bottomLeft, bottomRight);


            Matrix3 matrix = Matrix3.Identity;
            matrix.M31 = -quadToDraw.Centre.X;
            matrix.M32 = -quadToDraw.Centre.Y;


/*            quadToDraw *= matrix;*/

            DrawRectangle = quadToDraw.AABBFloat;

            Anchor = Anchor.Centre;
            OriginPosition = quadToDraw.Centre;
        }
        public QuadSprite(Quad quad)
        {
            quadToDraw = quad;
            DrawRectangle = quadToDraw.AABBFloat;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;

        }

        protected override Quad ComputeScreenSpaceDrawQuad(){
            return ToScreenSpace(DrawRectangle);
        }

        public Quad ToScreenSpace(Quad quad)
        {
            return (quadToDraw * DrawInfo.Matrix);
        }


        [BackgroundDependencyLoader]
        private void load(IRenderer renderer)
        {

            base.Texture = renderer.WhitePixel;
        }

        }
}
