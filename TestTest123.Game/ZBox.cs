using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osuTK;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using Veldrid;

namespace TestTest123.Game
{
    public partial class ZBox : Drawable
    {
        private Quad quad {  get; }
        private SimpleConvexPolygon polygon { get; set; }
        public ZBox()
        {
            this.RelativeSizeAxes = Axes.Both;
            Size = new Vector2(0.6f);

            Vector2 ver = new Vector2(2, 0);
            Vector2 ver1 = new Vector2(4, 2);
            Vector2 ver2 = new Vector2(4, 3);
            Vector2 ver3 = new Vector2(6, -2);
            Vector2[] ver4 = {ver, ver1, ver2, ver3};
            quad = new Quad(ver,ver2,ver3,ver1);

            this.polygon = new SimpleConvexPolygon(ver4);
            Colour = Colour4.AliceBlue;

            RelativeSizeAxes = Axes.Both;
        }

        [BackgroundDependencyLoader]
        private void load(IRenderer renderer)
        {
            renderer.DrawQuad(renderer.WhitePixel, quad, DrawColourInfo.Colour);
        }
        protected override DrawNode CreateDrawNode() => new PolygonDrawNode(this);

        private class PolygonDrawNode : DrawNode

        {
            protected new ZBox Source => (ZBox)base.Source;


            private SimpleConvexPolygon polygon;
            private Quad quad;
            public PolygonDrawNode(ZBox source)
                : base(source)
            {

                this.quad = Source.quad;
                this.polygon = Source.polygon;
            }

            public override void ApplyState()
            {
                base.ApplyState();
            }
            protected override void Draw(IRenderer renderer)
            {
                base.Draw(renderer);


                renderer.DrawQuad(renderer.WhitePixel, quad, DrawColourInfo.Colour);
                renderer.DrawClipped(ref polygon, renderer.WhitePixel, DrawColourInfo.Colour);
            }

        }
    }
}
