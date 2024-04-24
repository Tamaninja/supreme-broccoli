using osu.Framework.Allocation;
using osu.Framework.Extensions.MatrixExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Logging;
using osu.Framework.Platform.MacOS;
using osuTK;
using osuTK.Graphics;

namespace TestTest123.Game
{
    public abstract partial class Model : Drawable, ITexturedShaderDrawable
    {
        private int[][] indices = [];
        private Vector3[] vertices = [];
        private Vector3[] rotatedVertices = [];
        protected Vector3 Pos;
        protected Vector3 Scale3D = Vector3.One;

        protected Quaternion quat = Quaternion.Identity;

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



        public void SetScale(Vector3 scale)
        {
            Scale3D = scale;
        }
        public Matrix4 GetMatrix()
        {
            Matrix4 rot = Matrix4.CreateFromQuaternion(GetRotation());
            Matrix4 matrix = Matrix4.CreateTranslation(Pos);
            Matrix4 scale = Matrix4.CreateScale(Scale3D);

            return (rot * matrix * scale);
        }

        public virtual Quaternion GetRotation()
        {
            return quat;
        }

        public void SetRotation(float yaw, float pitch, float roll)
        {
            quat = new Quaternion(yaw, pitch, roll);
        }
        public void SetRotation(Vector3 rotation)
        {
            quat = new Quaternion(rotation);
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
            Pos = pos;
        }

        public Vector3 GetPosition() {

            return Pos;
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


    }
}
