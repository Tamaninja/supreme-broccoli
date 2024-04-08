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
    public partial class VString : Container
    {
        public VString()
        {
            Masking = true;
            RelativeSizeAxes = Axes.Both;
            RelativePositionAxes = Axes.Both;
            Anchor = Anchor.TopCentre;
            Origin = Anchor.Centre;
            Size = new Vector2(0.95f, 0.04f);
            CornerRadius = 10;
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            InternalChild = new Box
            {
                RelativeSizeAxes = Axes.Both,

            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
        }
    }
}
