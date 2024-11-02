
using Assimp;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Textures;
using osu.Framework.Logging;
using osuTK;
using Rocksmith2014.XML;

namespace TestTest123.Game
{
    public partial class PlaneDrawable : ModelDrawNode
    {

        public PlaneDrawable(SceneNode scene) : base(scene)
        {
           Scale.Value = (new Vector3(0.5f));
        }


        protected override Model LoadModel(IRenderer renderer)
        {
            return Model.BOX_3D(renderer);
        }

    }
}
