using System.Text;
using osuTK;

namespace TestTest123.Game
{
    internal class Utils
    {
        public static string ToString(Vector3[] vertices)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendJoin("\t", vertices);

            return (sb.ToString());
        }
    }
}
