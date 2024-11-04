using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Screens;
using osuTK;
using osuTK.Graphics;

namespace TestTest123.Game
{
    public partial class MainScreen : Screen
    {
        private SpriteText text;
        private Scene scene;

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren =
            [
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Color4.Black,
                },


                text = new SpriteText
                {
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(0.5f, 1f),
                    Y = 20,
                    Text = string.Empty,
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Font = FontUsage.Default.With(size: 30),
                    AllowMultiline = true
                },
                scene = [],

            ];
        }
    }
}
