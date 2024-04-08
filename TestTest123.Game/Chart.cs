using osu.Framework.Allocation;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osuTK;
using osuTK.Graphics;

namespace TestTest123.Game
{
    public partial class Chart : Container
    {
        private Note[] notes = new Note[1];
        private VString[] vStrings;
        private Container container;
        public Chart()
        {
            Anchor = Anchor.BottomLeft;
            Origin = Anchor.BottomLeft;
            RelativeSizeAxes = Axes.Both;
            Size = new Vector2(0.5f, 0.2f);
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            InternalChild = container = new Container
            {
                RelativeSizeAxes = Axes.Both,
            };
            container.Add(new Measure(60));
            initVStrings();
        }

        private VString[] initVStrings()
        {
            vStrings = new VString[4];
            var stringGap = container.Height / (vStrings.Length -1);
            for (int i = 0; i < vStrings.Length; i++)
            {
                container.Add(
                    vStrings[i] = new VString
                    {
                        Colour = Constants.VSTRINGCOLORS[i],
                        Position = new Vector2(0, stringGap * i)
                    }
                   );
            }
            return vStrings;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
        }
    }
}
