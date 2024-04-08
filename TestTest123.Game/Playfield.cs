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

namespace TestTest123.Game
{
    public partial class Playfield : Container
    {

        private GridContainer playfield;
        private int beatsAhead;
        private Fretboard fretboard;
        private Drawable[,] cells;
        public Playfield(int beatsAhead)
        {
            this.beatsAhead = beatsAhead;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            Masking = true;
            RelativeSizeAxes = Axes.Both;
            RelativePositionAxes = Axes.Both;
            Size = new Vector2(1f, 1f);
            Shear = new Vector2(0f, -0.025f);


            init();
        }

        private void init()
        {


            fretboard = new Fretboard(4, 12)
            {
                Depth = 1,
                Anchor = Anchor.BottomRight,
                Origin = Anchor.BottomRight,
                RelativeSizeAxes = Axes.Both,
                RelativePositionAxes = Axes.Both,
                Shear = new Vector2(-0.2f, 0f),
                Position = new Vector2(-0.15f, -0.05f),
                Size = new Vector2(0.8f, 0.25f),
            };

            playfield = new GridContainer()
            {
                Anchor = Anchor.BottomRight,
                Origin = Anchor.BottomRight,
                RelativeSizeAxes = Axes.Both,
                RelativePositionAxes = Axes.Both,
                Position = new Vector2(-0.15f, -0.05f),
                Shear = new Vector2(0.25f, 0f),
                Size = new Vector2(0.8f, 1.25f),
            };



            cells = new Drawable[beatsAhead, fretboard.Range];

            for (int r = 0; r < fretboard.Range; r++)
            {
                for (int c = 0; c < beatsAhead; c++)
                {
                    cells[c, r] = new Container
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
            }
            playfield.Content = cells.ToJagged();
            Add(playfield);
            Add(fretboard);
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
