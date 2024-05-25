
using System.Threading;
using Assimp;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Textures;
using osu.Framework.Logging;
using osuTK;
using osuTK.Graphics;
using osuTK.Graphics.OpenGL;


namespace TestTest123.Game
{
    public partial class Stage : Container<ThreeDimensionalDrawable>
    {
        public Camera Camera;
        public Stage()
        {

            RelativeSizeAxes = Axes.Both;
            Colour = Color4.AliceBlue.Opacity(0f);
            
        }


        [BackgroundDependencyLoader]
        private void load(ShaderManager shaders, IRenderer renderer, TextureStore textureStore)
        {

            Camera = new Camera();
            AddInternal(Camera);

            Box3D box = new Box3D(Camera);
            AddInternal(box);
/*            box.MoveTo(new Vector3(0,0,150), 15000, Easing.None);
            box.RotateTo(new Vector3(0, 0, 360), 15000, Easing.None);*/
        }

    }
}
