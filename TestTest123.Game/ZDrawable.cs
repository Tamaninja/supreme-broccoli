using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace TestTest123.Game
{
    public partial class ZDrawable : Sprite
    {
        protected Vector3[] Vertices {  get; set; }
        private Vector3[] projectedVertices;


        public ZDrawable(Vector3[] vertices){
            Vertices = vertices;
            projectedVertices = vertices;

            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;



            RelativePositionAxes = Axes.Both;
        }

        public Vector3[] ProjectedVertices()
        {
            return (projectedVertices);
        }

        public Vector3[] GetVertices()
        {
            return (Vertices);
        }

        public Vector3[] ProjectVertices(Matrix4 worldViewProjection, Camera camera) {
            for (int i = 0; i < Vertices.Length; i++)
            {
                projectedVertices[i] = Vector3.Project(camera.GetPos3D() - Vertices[i], 1, 1, DrawWidth, DrawHeight, 1, 5000, worldViewProjection);
            }

            return projectedVertices;
        }

    }
}
