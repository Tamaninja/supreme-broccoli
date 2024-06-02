
using System;
using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Rendering;
using osuTK;
using osuTK.Graphics;


namespace TestTest123.Game
{
    public partial class ThreeDimensionalStageDrawable : Container
    {
        public Camera Camera;
        public ThreeDimensionalStageDrawable()
        {
            RelativeSizeAxes = Axes.Both;
            Colour = Color4.AliceBlue.Opacity(0f);
        }

        public override bool PropagatePositionalInputSubTree => true;

        [BackgroundDependencyLoader]
        private void load(IRenderer renderer)
        {
            Camera = new Camera(50, 16/9, 1, 5000);
            AddInternal(Camera);

            Model box = Model.BOX_3D();

            Random rand = new Random();

            for (int i = 0; i < 77; i++)
            {
                int str = rand.Next(1, 5);
                ModelDrawable box3d = new ModelDrawable(box, this);
                AddInternal(box3d);
                box3d.BindCamera(Camera);

                box3d.Colour = Utils.StringColors(str);
                box3d.Position3D = new Vector3(new Random().Next(1, 12), str, 100);
                box3d.MoveToZ(0, 20000, Easing.Out);
            }


        }
    }
}
