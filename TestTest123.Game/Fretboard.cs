using osu.Framework.Allocation;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osuTK;
using osuTK.Graphics;
using osu.Framework.Layout;
using osu.Framework.Extensions;

namespace TestTest123.Game
{
    public partial class Fretboard : GridContainer
    {
        public int Range { get; set; }
        private int strings;
        private Drawable[,] cells;

        public Fretboard(int strings, int range)
        {

            this.Range = range;
            this.strings = strings;
            init();

        }

        private void init()
        {
            cells = new Drawable[strings, Range];

            for (int r = 0; r < Range; r++) {
                for (int c = 0; c < strings; c++) {
                    cells[c, r] = new Container
                    {
                        Colour = Constants.VSTRINGCOLORS[c],
                        RelativeSizeAxes = Axes.Both,
                        Child = new Box
                        {
                            RelativeSizeAxes = Axes.Both
                        }
                    };

                }
            }
            Content = cells.ToJagged();
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
