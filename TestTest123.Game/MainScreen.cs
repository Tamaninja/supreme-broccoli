using System.Text;
using ManagedBass;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osu.Framework.Logging;
using osu.Framework.Screens;
using osuTK;
using osuTK.Graphics;
using TestTest123.Game.Audio;

namespace TestTest123.Game
{
    public partial class MainScreen : Screen
    {
        private Camera camera;
        private SpriteText text;
        private Stage mainStage;

        [BackgroundDependencyLoader]
        private void load()
        {

            InternalChildren = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Color4.Black.Opacity(0.2f)
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
                mainStage = new Stage
                {
                    Child = new Note().GetChild()
                },
            };
            camera = new Camera(mainStage, text);

        }


    }
}
