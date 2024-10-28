
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
    public partial class NoteDrawable : ModelDrawNode
    {
        public static int PRELOAD_MS = 10000;
        public static int KEEPALIVE_MS = 10000;
        private static Colour4[] colorTable = [Colour4.Red, Colour4.Yellow, Colour4.Blue, Colour4.Orange];
        private Note note;
 
        public NoteDrawable(Note note, ThreeDimensionalDrawNode parent) : base(parent)
        {
            this.note = note;

            Position.Value = new Vector3(note.Fret * 2, -note.String, note.Time / 100);
            Scale.Value = (new Vector3(0.5f));
        }


        protected override Model LoadModel(IRenderer renderer)
        {
            return Model.MCQUEEN(renderer);
        }

    }
}
