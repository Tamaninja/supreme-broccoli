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
                    Y = 20,
                    Text = Vector3.Zero.ToString(),
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Font = FontUsage.Default.With(size: 40)
                },
                mainStage = new Stage
                {
                    Child = new Note().GetChild()
                },
            };
            camera = new Camera(mainStage, new Vector3(0,0,5), text);

        }


    }
}
