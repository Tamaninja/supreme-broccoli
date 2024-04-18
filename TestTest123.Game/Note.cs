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

namespace TestTest123.Game
{
    public partial class Note : Container
    {
        public Box3D Box { get; set; }

        public Note()
        {
            RelativeSizeAxes = Axes.Both;
            Colour = Color4.DodgerBlue.Opacity(0.5f);

            Vector2 topLeft = new Vector2(0, 0);
            Vector2 topRight = new Vector2(0, 200);
            Vector2 bottomLeft = new Vector2(200, 0);
            Vector2 bottomRight = new Vector2(200, 200);
            Quad quad = new Quad(topLeft, topRight, bottomLeft, bottomRight);
            Quad quad1 = new Quad(topLeft, topRight, bottomLeft, bottomRight);
            Vector3 v1 = new Vector3(-0.5f, -0.5f, -0.5f);  // Bottom-left-front corner
            Vector3 v2 = new Vector3(0.5f, -0.5f, -0.5f);  // Bottom-right-front corner
            Vector3 v3 = new Vector3(0.5f, -0.5f, 0.5f);  // Bottom-right-back corner
            Vector3 v4 = new Vector3(-0.5f, -0.5f, 0.5f);  // Bottom-left-back corner
            Vector3 v5 = new Vector3(-0.5f, 0.5f, -0.5f);  // Top-left-front corner
            Vector3 v6 = new Vector3(0.5f, 0.5f, -0.5f);  // Top-right-front corner
            Vector3 v7 = new Vector3(0.5f, 0.5f, 0.5f);  // Top-right-back corner
            Vector3 v8 = new Vector3(-0.5f, 0.5f, 0.5f);  // Top-left-back corner

            Box3D box = new Box3D(v1, v2, v3, v4, v5, v6, v7, v8);

            Box = box;
        }

        public Model GetChild()
        {
            return (Box);
        }


        [BackgroundDependencyLoader]
        private void load(IRenderer renderer)
        {

        }
    }
}
