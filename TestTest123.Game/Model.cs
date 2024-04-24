using osu.Framework.Allocation;
using osu.Framework.Extensions.MatrixExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Logging;
using osuTK;

namespace TestTest123.Game
{
    public abstract partial class Model : Drawable, ITexturedShaderDrawable
    {
        private int[][] indices = [];
        private Vector3[] vertices = [];
        private Vector3[] rotatedVertices = [];
        protected Vector3 Pos;

        protected Quaternion quat = Quaternion.Identity;
        protected Vector3 rotation;
        private Matrix4 modelMatrix = Matrix4.Identity;


        public Model(Vector3 pos)
        {
            SetPosition(pos);
            
            Init();
        }
        [BackgroundDependencyLoader]
        private void load(ShaderManager shaders)
        {
            TextureShader = shaders.Load(VertexShaderDescriptor.TEXTURE_3, FragmentShaderDescriptor.TEXTURE);
        }

        public IShader TextureShader { get; protected set; }



        public Matrix4 GetMatrix(){

            return modelMatrix;
        }

        public Vector3 GetRotation()
        {
            return rotation;
        }

        public void SetRotation(float yaw, float pitch, float roll)
        {
            rotation = new Vector3(yaw, pitch, roll);
            quat = new Quaternion(yaw, pitch, roll);
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
            modelMatrix = Matrix4.CreateTranslation(pos);
            Pos = pos;
        }

        public Vector3 GetPosition() {

            return modelMatrix.ExtractTranslation();
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



        protected abstract void Init();

        protected void Rotate(float yaw, float pitch, float roll)
        {

            Vector3 vector = new Vector3(yaw, pitch, roll);
            rotation += vector;


        }


    }
}
