using osu.Framework.Allocation;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osuTK;
using osuTK.Graphics;
using System.Drawing;
using osu.Framework.Extensions.Color4Extensions;

namespace TestTest123.Game
{
    public partial class Measure : CompositeDrawable
    {
        private Container box;
        private int duration;
        private int beatsPerMinute;
        private int beats = 4;
        private int approachRate = 150;

        public Measure (int beatsPerMinute)
        {
            RelativeSizeAxes = Axes.Both;
            duration = (60 / beatsPerMinute) * beats;
            this.beatsPerMinute = beatsPerMinute;
        }

        private void addZDimension()
        {

            int length = duration * approachRate;

            Container playField = new Container
            {
                Masking = true,
                Anchor = Anchor.TopLeft,
                Origin = Anchor.BottomLeft,
                RelativeSizeAxes = Axes.X,
                Shear = new Vector2(0, 0),
                Size = new Vector2(1, 2 *length),


            };
            box.Add(playField);
        } 

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            Box measureStart = new Box
            {
                RelativeSizeAxes = Axes.Both,
            };

            InternalChild = box = new Container
            {
                Colour = Color4.Brown.Opacity(0.5f),
                RelativeSizeAxes = Axes.Both,
                Shear = new Vector2(0, -0.2f)
           };
            box.Add(measureStart);

            addZDimension();
        }

        private void divideBeats(Container container)
        {
            for (int i = 0; i < beats; i++)
            {
                Box box = new Box
                {
                    Anchor = Anchor.BottomLeft,
                    Origin = Anchor.BottomLeft,
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2 (1, 0.03f),
                    Position = -(container.Size / beats) * i
                };
                container.Add(box);
            }
        }

        private void divideFrets(Container container, int range)
        {
            for (int i = 0; i < range; i++)
            {
                Box box = new Box
                {
                    Anchor = Anchor.BottomLeft,
                    Origin = Anchor.BottomLeft,
                    RelativePositionAxes = Axes.Both,
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(0.03f, 1),
                    Position = new Vector2((container.Size.X/range) * i, 0)
                };
                container.Add(box);
            }
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
        }
    }
}
