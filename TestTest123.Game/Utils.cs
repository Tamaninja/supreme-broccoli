using System.Text;
using Assimp;
using osu.Framework.Logging;
using osuTK;
using osuTK.Graphics;

namespace TestTest123.Game
{
    public static class Utils
    {

        public static Color4 StringColors(int i)
        {
            switch (i)
            {
                case 3: return Color4.Orange;
                case 2: return Color4.Blue;
                case 1: return Color4.Yellow;
                case 0: return Color4.Red;
                    default: return Color4.White;
            }
        }
        public static Color4 FromAssimp(this Color4D assimp)
        {


            return new Color4(assimp.R, assimp.G, assimp.B, assimp.A);
        }

        public static string ToString(Vector3[] vertices)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendJoin("\t", vertices);

            return (sb.ToString());
        }
    }
}
