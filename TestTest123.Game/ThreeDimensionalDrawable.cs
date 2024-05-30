
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
using osuTK;
using osuTK.Graphics.OpenGL;

namespace TestTest123.Game
{
    public partial class ThreeDimensionalDrawable : PoolableDrawable
    {
        public static Vector3 WORLD_UP => Vector3.UnitY;
        public static Vector3 WORLD_FORWARD => Vector3.UnitZ;
        public static Vector3 WORLD_RIGHT => Vector3.UnitX;

        private Vector3 relativeForward = WORLD_FORWARD;
        private Vector3 relativeRight = WORLD_RIGHT;
        private Vector3 relativeUp = WORLD_UP;

        private Vector3 position = Vector3.Zero;
        private Vector3 rotation = Vector3.Zero;
        private Vector3 scale = Vector3.One;

        private Bindable<Matrix4> localMatrix;

        public virtual Matrix4 GetMatrix() => localMatrix.Value;
        public virtual Bindable<Matrix4> GetMatrixBindable => localMatrix;

        public virtual Vector3 Forward
        {
            get => relativeForward;
            set
            {
                relativeForward = value;
                Invalidate(Invalidation.MiscGeometry);
            }
        }

        public virtual Vector3 Right => relativeRight;
        public virtual Vector3 Up => relativeUp;

        public virtual Vector3 Scale3D
        {
            get => scale;
            set
            {
                scale = value;
                Invalidate(Invalidation.MiscGeometry);
            }
        }

        public virtual Vector3 Position3D
        {
            get => position;
            set
            {
                position = value;
                Invalidate(Invalidation.MiscGeometry);
            }
        }

        public virtual Vector3 Rotation3D
        {
            get => rotation;
            set
            {
                rotation = value;
                Invalidate(Invalidation.MiscGeometry);
            }
        }

        public ThreeDimensionalDrawable()
        {
            localMatrix = new Bindable<Matrix4>(Matrix4.Identity);
            RelativeSizeAxes = Axes.Both;
        }


        protected override bool OnInvalidate(Invalidation invalidation, InvalidationSource source)
        {

            if (invalidation.HasFlag(Invalidation.MiscGeometry))
            {
                relativeRight = Vector3.Normalize(Vector3.Cross(WORLD_UP, relativeForward));
                relativeUp = Vector3.Cross(relativeForward, relativeRight);


                Matrix4 scaleMatrix = Matrix4.CreateScale(scale);
                Matrix4 translation = Matrix4.CreateTranslation(position);
                Matrix4 rotationX = Matrix4.CreateRotationX(rotation.X);
                Matrix4 rotationY = Matrix4.CreateRotationY(rotation.Y);
                Matrix4 rotationZ = Matrix4.CreateRotationZ(rotation.Z);

                localMatrix.Value = (scaleMatrix * (rotationX * rotationY * rotationZ) * translation);
            }



            return(base.OnInvalidate(invalidation, source));
        }

        [BackgroundDependencyLoader]
        private void load(ShaderManager shaders, IRenderer renderer, TextureStore textureStore)
        {
        }
    }
}
