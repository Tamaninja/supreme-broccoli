
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
    public partial class NoteDrawable : Model
    {
        public static int PRELOAD_MS = 10000;
        public static int KEEPALIVE_MS = 10000;
        public static Colour4[] ColorTable = [Colour4.Red, Colour4.Yellow, Colour4.Blue, Colour4.Orange];

        public NoteDrawable(SceneNode scene) : base(Model.BOX_3D, scene)
        {
        }


    }
}
