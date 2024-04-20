using osu.Framework.Graphics.Sprites;
using osuTK;

namespace TestTest123.Game
{
    public abstract partial class Model : Sprite
    {
        private Vector3[] vertices = [];
        private Vector3[] rotatedVertices = [];
        private Vector3 pos;
        private int[][] indices = [];
        private Quaternion orientation = Quaternion.Identity;

        public Model(Vector3 pos)
        {
            SetPosition(pos);
            Init();
        }

        protected Quaternion GetOrientation() {

            return orientation;
        }

        public void Rotate(float yaw, float pitch, float roll)
        {
            orientation *= new Quaternion(yaw, pitch, roll);            
        }

        public void SetRotation(float yaw, float pitch, float roll)
        {
            orientation = new Quaternion(yaw, pitch, roll);
        }
        public void ClearRotation()
        {
            SetRotation(0, 0, 0);
        }
        protected void SetIndices(int[][] indices)
        {
            this.indices = indices;
        }

        public int[][] GetIndices()
        {
            return indices;
        }

        public void SetPosition(Vector3 pos)
        {
            this.pos = pos;
        }

        public Vector3 GetPosition() {

            return pos;
        }

        public Vector3[] GetVertices()
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                rotatedVertices[i] = Vector3.Transform(vertices[i], orientation);
            }
            return rotatedVertices;
        }
        public void SetVertices(Vector3[] vertices)
        {
            this.vertices = vertices;
            rotatedVertices = vertices;
        }

        public void MoveBy(Vector3 offset)
        {
            SetPosition(pos + offset);
        }

        protected abstract void Init();


    }
}
