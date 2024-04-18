using osu.Framework.Allocation;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osuTK;
using osuTK.Graphics;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Extensions;
using osu.Framework.Graphics.Primitives;

namespace TestTest123.Game
{
    public partial class Playfield : Container
    {

        private Model zDrawable;
        private GridContainer playfield;
        private Drawable[,] lanes;
        public new RectangleF DrawRectangle;
        public Playfield(Vector2 topLeft, Vector2 topRight, Vector2 bottomLeft, Vector2 bottomRight)
        {
            DrawRectangle = new Quad(topLeft, topRight, bottomLeft, bottomRight).AABBFloat;

            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;


            RelativeSizeAxes = Axes.Both;
            RelativePositionAxes = Axes.Both;


            init();
        }
        public Model GetZDrawable()
        {
            return (zDrawable);
        }
        private void init()
        {

            playfield = new GridContainer()
            {
                RelativeSizeAxes = Axes.Both,
                RelativePositionAxes = Axes.Both,
            };



            lanes = new Drawable[1, 12];
            for (int i = 0; i < lanes.Length; i++)
            {
                lanes[0, i] = new Container
                    {
                        RelativeSizeAxes = Axes.Both,
                        Child = new Box
                        {
                            Colour = Color4.AliceBlue.Opacity(0.25f),
                            Margin = new MarginPadding(0.2f),
                            RelativeSizeAxes = Axes.Both
                        }
                    };
            }
            playfield.Content = lanes.ToJagged();
            
        }

        protected override void UpdateAfterChildren()
        {
            base.UpdateAfterChildren();
            Vector3[] vertices = zDrawable.GetVertices();

            DrawRectangle = new Quad(vertices[0].Xz, vertices[1].Xz, vertices[2].Xz, vertices[3].Xz).AABBFloat;

        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
        }
    }
}
