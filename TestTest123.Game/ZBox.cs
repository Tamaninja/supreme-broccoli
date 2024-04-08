using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osuTK;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Sprites;
using System.Runtime.CompilerServices;
using System;
using System.Diagnostics;
using osuTK.Audio.OpenAL;

namespace TestTest123.Game
{
    public partial class ZBox : Sprite
    {
        private Quad quad {  get; }
        private float xFactor {  get; set; }
        public ZBox(float xFactor)
        {
            if (xFactor < 0 || xFactor > 1) return;

            this.xFactor = xFactor;
            RelativeSizeAxes = Axes.Both;
        }

        [BackgroundDependencyLoader]
        private void load(IRenderer renderer)
        {
            base.Texture = renderer.WhitePixel;
        }
        public override bool Contains(Vector2 screenSpacePos) => quad.Contains(screenSpacePos);

        protected override DrawNode CreateDrawNode() => new QuadDrawNode(this, xFactor);

        private class QuadDrawNode : SpriteDrawNode

        {
            private float xFactor;
            public QuadDrawNode(ZBox source, float xFactor)
                : base(source)
            {
                this.xFactor = xFactor;
            }

            protected override void Blit(IRenderer renderer)
            {
                if (DrawRectangle.Width == 0 || DrawRectangle.Height == 0)
                    return;
                
                renderer.DrawQuad(Texture, toZBox(ScreenSpaceDrawQuad), DrawColourInfo.Colour, null, null,
                    new Vector2(InflationAmount.X / DrawRectangle.Width, InflationAmount.Y / DrawRectangle.Height),
                    null, TextureCoords);

            }
            private Quad toZBox(Quad q) {
                float gap = (q.Width * xFactor) / 2;
                Quad quad = new Quad(
                    new Vector2(q.TopLeft.X + gap, q.TopLeft.Y),
                    new Vector2(q.TopRight.X - gap, q.TopRight.Y),
                    q.BottomLeft,
                    q.BottomRight
                    );
                return (quad);
            } 


            /*            protected override void BlitOpaqueInterior(IRenderer renderer)
                        {
                            if (DrawRectangle.Width == 0 || DrawRectangle.Height == 0)
                                return;

                            if (renderer.IsMaskingActive)
                            {
                                renderer.DrawClipped(ref quad, Texture, DrawColourInfo.Colour);
                            }
                            else
                            {
                                renderer.DrawQuad(Texture, quad, DrawColourInfo.Colour);
                            }
                        }*/
        }
    }
}
