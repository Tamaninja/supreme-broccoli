using System;
using System.Collections.Generic;
using Assimp;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shaders;
using osuTK;

namespace TestTest123.Game
{
    public abstract partial class Model : Drawable, ITexturedShaderDrawable
    {
        protected Vector3 Scale3D = Vector3.One;
        protected List<Mesh> Meshes;
        protected new Vector3 Position = Vector3.Zero;
        protected AssimpContext Importer;

        public Model(string filepath = null)
        {
            Importer = new AssimpContext();
            if (filepath != null)
            {
                Scene scene = Importer.ImportFile(filepath, PostProcessSteps.Triangulate | PostProcessSteps.MakeLeftHanded);
                Meshes = scene.Meshes;
            }


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

            Vector3 temp = new Vector3(Position.X, Position.Y, Position.Z);
            Matrix4 matrix = Matrix4.CreateTranslation(temp);
            Matrix4 scale = Matrix4.CreateScale(Scale3D);

            return (matrix * scale);
        }


        public void SetPosition(Vector3 pos)
        {
           Position = pos;
        }

        public Vector3 GetPosition() {

            return Position;
        }

        public List<Mesh> GetMeshes()
        {
            return Meshes;
        }

    }
}
