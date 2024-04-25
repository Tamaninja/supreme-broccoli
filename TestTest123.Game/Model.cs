using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shaders;
using osuTK;

namespace TestTest123.Game
{
    public abstract partial class Model : Drawable, ITexturedShaderDrawable
    {
        private int[][] indices = [];
        private Vector3[] vertices = [];
        protected Vector3 Pos;
        protected Vector3 Scale3D = Vector3.One;

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
            Matrix4 matrix = Matrix4.CreateTranslation(Pos);
            Matrix4 scale = Matrix4.CreateScale(Scale3D);

            return (matrix * scale);
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
            return vertices;
        }
        public void SetVertices(Vector3[] vertices)
        {
            this.vertices = vertices;
        }


        protected abstract void Init();


    }
}
