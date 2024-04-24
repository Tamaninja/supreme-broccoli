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
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Shaders;
using osuTK.Graphics.OpenGL;

namespace TestTest123.Game
{
    public partial class Note : Container
    {
        public Box3D Box { get; set; }

        public Note()
        {
            RelativeSizeAxes = Axes.Both;
            Colour = Color4.DodgerBlue.Opacity(0.5f);


            Box3D box = new Box3D(new Vector3(0, 0, 0));

            Box = box;
        }

        public Model GetChild()
        {
            return (Box);
        }



    }
}
