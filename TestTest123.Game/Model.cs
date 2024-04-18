using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace TestTest123.Game
{
    public partial class Model : Sprite
    {
        protected Vector3[] Vertices;

        public Model(Vector3[] vertices)
        {
            RelativePositionAxes = Axes.Both;

            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;

            Vertices = vertices;

        }

        public Vector3[] GetVertices()
        {
            return Vertices;
        }
    }
}
