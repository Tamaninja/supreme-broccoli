﻿
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
                box3d.RotateTo(new Vector3(rand.Next(1, 10), rand.Next(1, 10), rand.Next(1, 10)), 23000);
                box3d.Delay(i * 250 * str).MoveToZ(0, 20000, Easing.In);
            }


        }
    }
}
