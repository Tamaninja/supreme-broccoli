using System;
using System.Collections.Generic;
using Assimp;
using osu.Framework.Allocation;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Logging;
using osuTK;
using static osuTK.Graphics.OpenGL.GL;

namespace TestTest123.Game
{
    public abstract partial class Model : Drawable, ITexturedShaderDrawable
    {
        protected new Vector3 Scale = Vector3.One;
        protected new Vector3 Position = Vector3.Zero;
        protected Vector3 Forward = Vector3.UnitZ;

        protected AssimpContext Importer;
        protected float Yaw = 0;
        protected float Pitch = 0;
        protected float Roll = 0;
        protected new Vector3 Rotation = Vector3.Zero;
        public Scene SceneInfo;

        public IShader TextureShader {  get; set; }
        public List<Texture> Textures = new List<Texture>();
        
        

        public Model(string filepath = null)
        {
            Importer = new AssimpContext();
            if (filepath != null)
            {
                SceneInfo = Importer.ImportFile(filepath, PostProcessSteps.Triangulate);

            }

        }
        [BackgroundDependencyLoader]
        private void load(ShaderManager shaders, TextureStore textureStore)
        {
            TextureShader = shaders.Load("nino", "nino");

            if (SceneInfo == null) return;
            if (SceneInfo.HasMaterials)
            {
                for (int i = 0; i < SceneInfo.MaterialCount; i++)
                {

                    SceneInfo.Materials[i].GetAllMaterialTextures().ForEach(s =>
                    {

                        Textures.Add(textureStore.Get(s.FilePath, WrapMode.None, WrapMode.None));
                        Logger.LogPrint(s.WrapModeU.ToString() + " " + s.WrapModeV.ToString());
                    });
                }

            }
        }

        

        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt(Position, Position + Forward, Vector3.UnitY);
        }

        public void SetScale(Vector3 scale)
        {
            Scale = scale;
        }
        public Matrix4 GetLocalMatrix()
        {
            Matrix4 scale = Matrix4.CreateScale(Scale);

            Matrix4 translation = Matrix4.CreateTranslation(Position);

            Matrix4 rotation = Matrix4.CreateFromAxisAngle(Forward, MathF.Acos(Vector3.Dot(Forward, Vector3.UnitZ))); //NEEDS FIXING

            return (rotation * scale * translation);
        }


        public void SetPosition(Vector3 pos)
        {
           Position = pos;
        }

        public Vector3 GetPosition() {

            return Position;
        }

    }
}
