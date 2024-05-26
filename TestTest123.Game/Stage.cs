
using System;
using System.Threading;
using Assimp;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Pooling;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Textures;
using osu.Framework.Logging;
using osuTK;
using osuTK.Graphics;
using osuTK.Graphics.OpenGL;


namespace TestTest123.Game
{
    public partial class Stage : Container
    {
        private Camera camera;

        public Stage()
        {
            RelativeSizeAxes = Axes.Both;
            Colour = Color4.AliceBlue.Opacity(0f);
        }

        [BackgroundDependencyLoader]
        private void load(ShaderManager shaders, IRenderer renderer, TextureStore textureStore)
        {
            camera = new Camera();
            AddInternal(camera);
            DrawablePool<Box3D> notepool = new DrawablePool<Box3D>(1);
            AddInternal(notepool);

            for (int i = 0;  i < 10; i++)
            {
                
                notepool.Get((s) =>
                {
                    AddInternal(s);
                    s.Colour = Color4.DarkOrange;
                    s.Camera = camera;
                    s.Scale3D = new Vector3(10);
                    s.MoveTo(new Vector3(10, 10, 10), new Random().Next(), Easing.None);
                    s.RotateTo(new Vector3(0, 0, MathHelper.DegreesToRadians(new Random().Next(130, 1000))), 15000, Easing.None);

                });
            }


        }
    }
}
