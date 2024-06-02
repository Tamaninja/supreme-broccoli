
using Assimp;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Pooling;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Textures;
using osu.Framework.Layout;
using osu.Framework.Logging;
using osu.Framework.Utils;
using osuTK;
using osuTK.Graphics.OpenGL;

namespace TestTest123.Game
{
    public partial class ThreeDimensionalDrawable : PoolableDrawable
    {
        public override Vector2 Size => Vector2.One;

        public static Vector3 WORLD_FORWARD = Vector3.UnitZ;
        public static Vector3 WORLD_UP = Vector3.UnitY;

        private Vector3 relativeForward = WORLD_FORWARD;



        private readonly LayoutValue<Matrix4> rotationMatrixBacking = new(Invalidation.MiscGeometry | Invalidation.DrawInfo);

        private Vector3 position = Vector3.Zero;
        private Vector3 rotation = Vector3.Zero;
        private Vector3 scale = Vector3.One;


        public Bindable<Matrix4> LocalMatrix;

        public Bindable<Matrix4> CameraViewProjection { get; }
        public virtual Matrix4 RotationMatrix => rotationMatrixBacking.IsValid ? rotationMatrixBacking : rotationMatrixBacking.Value = createRotationMatrix();
                
        public virtual Matrix4 Matrix => LocalMatrix.Value;

        public Vector3 Right => Vector3.Normalize(Vector3.Cross(WORLD_UP, Forward));
        public Vector3 Up => Vector3.Cross(Forward, Right);
        public virtual Vector3 Forward
        {

            get => relativeForward;
            set
            {
                if (relativeForward == value) return;

                relativeForward = value;
                UpdateMatrix();
            }
        }

        public virtual Vector3 Rotation3D
        {
            get => rotation;
            set
            {
                if (rotation == value) return;

                rotation = value;
                rotationMatrixBacking.Invalidate();
                UpdateMatrix();

            }
        }
        public virtual Vector3 Position3D
        {
            get => position;
            set
            {
                if (position == value) return;

                position = value;
                UpdateMatrix();
            }
        }
        public virtual Vector3 Scale3D
        {
            get => scale;
            set
            {
                if (scale == value) return;

                scale = value;
                UpdateMatrix();
            }
        }
        public ThreeDimensionalDrawable()
        {
            CameraViewProjection = new Bindable<Matrix4>(Matrix4.Identity);
            LocalMatrix = new Bindable<Matrix4>(Matrix4.Identity);

            CameraViewProjection.BindValueChanged((t) => Invalidate(Invalidation.DrawNode));
            LocalMatrix.BindValueChanged((t) => Invalidate(Invalidation.DrawNode));;

       }


        [BackgroundDependencyLoader]
        private void load(ShaderManager shaders, IRenderer renderer, TextureStore textureStore)
        {
        }


        public void BindCamera(Camera camera)
        {
            CameraViewProjection.BindTo(camera.CameraViewProjection);
        }

        public virtual void UpdateMatrix()
        {            
            LocalMatrix.Value = (
                Matrix4.CreateScale(scale)
                * RotationMatrix
                * Matrix4.CreateTranslation(position));
        }

        private Matrix4 createRotationMatrix()
        {
            Matrix4 rotationX = Matrix4.CreateRotationX(rotation.X);
            Matrix4 rotationY = Matrix4.CreateRotationY(rotation.Y);
            Matrix4 rotationZ = Matrix4.CreateRotationZ(rotation.Z);

            return (rotationX * rotationY * rotationZ);
        }
    }
}
