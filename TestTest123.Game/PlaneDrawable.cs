
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

        public PlaneDrawable(ThreeDimensionalDrawNode parent) : base(parent)
        {
           Scale.Value = (new Vector3(0.5f));
        }


        protected override Model LoadModel(IRenderer renderer)
        {
            return Model.MCQUEEN(renderer);
        }

    }
}
