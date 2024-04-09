using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Screens;
using osuTK;
using osuTK.Graphics;

namespace TestTest123.Game
{
    public partial class MainScreen : Screen
    {
        private Camera camera;
        private SpriteText text;

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Color4.Black
                },

                text = new SpriteText
                {
                    Y = 20,
                    Text = Vector3.Zero.ToString(),
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Font = FontUsage.Default.With(size: 40)
                },
                camera = new Camera(new Vector3(0, 0, -50), text),

/*                new Playfield(8)
                {

                }*/
            };
            camera.Add(new Note(camera, new Vector3(0, 0, 0)));
            camera.Add(new Note(camera, new Vector3(0, 0, 1)));

        }


    }
}
