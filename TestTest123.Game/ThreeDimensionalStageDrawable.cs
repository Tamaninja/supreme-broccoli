
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using Assimp;
using NUnit.Framework;
using osu.Framework;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Pooling;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Shaders.Types;
using osu.Framework.Graphics.Textures;
using osu.Framework.IO.Stores;
using osu.Framework.Logging;
using osu.Framework.Platform;
using osuTK;
using osuTK.Graphics;
using osuTK.Graphics.OpenGL;
using TestTest123.Game.Vertices;


namespace TestTest123.Game
{
    public partial class ThreeDimensionalStageDrawable : Container
    {
        public Dictionary<Model, List<MaterialDrawable>> Materials = [];
        public Camera Camera;
        public ThreeDimensionalStageDrawable()
        {
            RelativeSizeAxes = Axes.Both;
            Colour = Color4.AliceBlue.Opacity(0f);
        }



        [BackgroundDependencyLoader]
        private void load(IRenderer renderer)
        {
            Camera = new Camera();
            AddInternal(Camera);

            Model box = Model.BOX_3D();


            for (int i = 0; i < 13; i++)
            {
                ModelDrawable box3d = new ModelDrawable(box, this);
                AddInternal(box3d);

                box3d.Colour = Color4.DarkOrange;
/*                box3d.Position3D = new Vector3(new Random().Next(1, 24), new Random().Next(1, 4), 100);
                box3d.Delay(i * 500).MoveToZ(0, 50000, Easing.None).Then().Expire();*/
            }


        }
    }
}
