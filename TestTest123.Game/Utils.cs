using System.Text;
using Assimp;
using osu.Framework.Logging;
using osuTK;
using osuTK.Graphics;

namespace TestTest123.Game
{
    public static class Utils
    {
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
