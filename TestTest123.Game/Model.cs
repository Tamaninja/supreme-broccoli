using osu.Framework.Graphics.Sprites;
using osuTK;

namespace TestTest123.Game
{
    public abstract partial class Model : Sprite
    {
        protected Vector3[] Vertices = [];
        protected Vector3 Pos3D;
        protected int[][] Indices = [];

        public Model(Vector3 pos)
        {
            SetPosition(pos);
            Init();
        }
        protected void SetIndices(int[][] indices)
        {
            Indices = indices;
        }

        public int[][] GetIndices()
        {
            return Indices;
        }

        public void SetPosition(Vector3 pos)
        {
            Pos3D = pos;
        }


        public Vector3 GetPosition() {
            return Pos3D;
        }

        public Vector3[] GetVertices()
        {
            return Vertices;
        }
        public void SetVertices(Vector3[] vertices)
        {
            Vertices = vertices;
        }

        public void MoveBy(Vector3 offset)
        {
            SetPosition(Pos3D + offset);
        }

        protected abstract void Init();
    }
}
