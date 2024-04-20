using osu.Framework.Extensions.MatrixExtensions;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace TestTest123.Game
{
    public abstract partial class Model : Sprite
    {
        private int[][] indices = [];
        private Vector3[] vertices = [];
        private Vector3[] rotatedVertices = [];
        private Vector3 pos;

        private Vector3 viewDirection;
        protected Vector3 rotation;

        public Model(Vector3 pos)
        {
            SetPosition(pos);
            Init();
        }

        public void ResetViewDirection()
        {
            viewDirection = Vector3.UnitZ;
        }

        public void SetViewDirection(Vector3 newDirection)
        {
            viewDirection = newDirection;

        }
        public Vector3 GetViewDirection()
        {

            return (viewDirection);

        }

        public Vector3 GetRotation()
        {
            return rotation;
        }

        public void SetRotation(float yaw, float pitch, float roll)
        {
            rotation = new Vector3(yaw, pitch, roll);
        }
        public void ClearRotation()
        {
            SetRotation(0, 0, 0);
            ResetViewDirection();

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
            return rotatedVertices;
        }
        public void SetVertices(Vector3[] vertices)
        {
            this.vertices = vertices;
            rotatedVertices = vertices;
        }

        public void MoveBy(Vector3 offset)
        {
            offset *= rotation;
            SetPosition(pos + offset);
        }

        protected abstract void Init();

        protected void Rotate(float yaw, float pitch, float roll)
        {

            Vector3 vector = new Vector3(yaw, pitch, roll);
            rotation += vector;


        }


    }
}
