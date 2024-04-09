using osu.Framework.Allocation;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osuTK;
using osuTK.Graphics;
using osu.Framework.Graphics.Rendering;
using System;
using osu.Framework.Extensions.Color4Extensions;
using System.ComponentModel.DataAnnotations;

namespace TestTest123.Game
{
    public partial class Note : ZDrawable
    {


        public Note(Camera camera, Vector3 xyz3D) : base(camera, xyz3D)
        {
            Colour = Color4.DodgerBlue.Opacity(0.5f);
            Masking = true;
            RelativeSizeAxes = Axes.Both;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            CornerRadius = 10;
        }


        [BackgroundDependencyLoader]
        private void load(IRenderer renderer)
        {
            AddInternal(new Box()
            {
                RelativeSizeAxes = Axes.Both,
            });
            AddInternal(new Box()
            {
                RelativeSizeAxes = Axes.Both,

            });

        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
        }
    }
}
