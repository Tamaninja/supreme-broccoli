
using osu.Framework.Graphics.Rendering;
using osuTK;
using Rocksmith2014.XML;

namespace TestTest123.Game
{
    public partial class NoteDrawable : ModelDrawable
    {
        public NoteDrawable()
        {

        }

        protected override Model LoadModel(IRenderer renderer)
        {
            return Model.BOX_3D(renderer);
        }

    }
}
