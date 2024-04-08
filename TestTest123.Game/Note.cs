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

namespace TestTest123.Game
{
    public partial class Note : ZDrawable
    {

        public Note(Camera camera, Vector3 xyz3D) : base(camera, xyz3D)
        {


            Masking = true;
            RelativeSizeAxes = Axes.Both;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            CornerRadius = 10;
        }


        [BackgroundDependencyLoader]
        private void load(IRenderer renderer)
        {
            InternalChild = new Box
            {
                RelativeSizeAxes = Axes.Both,

            };
            Set3DPos(new Vector3(0,0,10));
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
        }
    }
}
