
using Assimp;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Pooling;
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
        public static Vector3 WORLD_FORWARD = Vector3.UnitZ;
        public static Vector3 WORLD_UP = Vector3.UnitY;

        private Vector3 relativeForward = WORLD_FORWARD;


        private Vector3 position = Vector3.Zero;
        private Vector3 rotation = Vector3.Zero;
        private Vector3 scale = Vector3.One;

        private Bindable<Matrix4> localMatrix;

        public Bindable<Matrix4> CameraViewProjection { get; }
        public virtual Matrix4 GetMatrix() => localMatrix.Value;

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
            localMatrix = new Bindable<Matrix4>(Matrix4.Identity);

            CameraViewProjection.BindValueChanged((t) => Invalidate(Invalidation.DrawNode));
            localMatrix.BindValueChanged((t) => Invalidate(Invalidation.MiscGeometry | Invalidation.DrawNode));;
            ;

            RelativeSizeAxes = Axes.Both;
        }

        public virtual void UpdateMatrix()
        {


                Matrix4 scaleMatrix = Matrix4.CreateScale(scale);
                Matrix4 translation = Matrix4.CreateTranslation(position);
                Matrix4 rotationX = Matrix4.CreateRotationX(rotation.X);
                Matrix4 rotationY = Matrix4.CreateRotationY(rotation.Y);
                Matrix4 rotationZ = Matrix4.CreateRotationZ(rotation.Z);

            localMatrix.Value = (scaleMatrix * (rotationX * rotationY * rotationZ) * translation);
            Invalidate(Invalidation.DrawNode, InvalidationSource.Parent);
        }

        [BackgroundDependencyLoader]
        private void load(ShaderManager shaders, IRenderer renderer, TextureStore textureStore)
        {
        }


        public void BindCamera(Camera camera)
        {
            CameraViewProjection.BindTo(camera.CameraViewProjection);
        }
    }
}
