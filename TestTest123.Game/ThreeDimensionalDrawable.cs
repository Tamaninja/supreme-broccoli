
using Assimp;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Pooling;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics.Transforms;
using osu.Framework.Layout;
using osu.Framework.Logging;
using osuTK;
using osuTK.Graphics.OpenGL;

namespace TestTest123.Game
{
    public partial class ThreeDimensionalDrawable : PoolableDrawable
    {
        private Vector3 forward;
        private Vector3 position;
        private Vector3 rotation;
        private Vector3 scale;

        private Bindable<Matrix4> localMatrix { get; set; } = new Bindable<Matrix4>(Matrix4.Identity);

        public Matrix4 LocalMatrix
        {
            get
            {
                updateMatrix();
                return (localMatrix.Value);
            }
            set
            {

                localMatrix.Value = value;
                RequiresRecalculation = true;

            }
        }

        protected bool RequiresRecalculation;

        public virtual Vector3 Forward
        {
            get => forward;
            set
            {
                forward = value;
                RequiresRecalculation = true;
            }
        }

        public virtual Vector3 Right => Vector3.Normalize(Vector3.Cross(Vector3.UnitY, Forward));
        public virtual Vector3 Up => Vector3.Cross(Forward, Right);

        public virtual Vector3 Scale3D
        {
            get => scale;
            set
            {
                scale = value;
                RequiresRecalculation = true;
            }
        }

        public virtual Vector3 Position3D
        {
            get => position;
            set
            {
                position = value;
                RequiresRecalculation = true;
            }
        }

        public virtual Vector3 Rotation3D
        {
            get => rotation;
            set
            {
                rotation = value;
                RequiresRecalculation = true;
            }
        }

        public ThreeDimensionalDrawable()
        {
            

            RelativeSizeAxes = Axes.Both;
            
            scale = Vector3.One;
            position = Vector3.Zero;
            rotation = Vector3.Zero;
            forward = Vector3.UnitZ;

            
            RequiresRecalculation = true;
        }

        private void updateMatrix()
        {
            if (!RequiresRecalculation) return;

            Matrix4 scaleMatrix = Matrix4.CreateScale(Scale3D);

            Matrix4 translation = Matrix4.CreateTranslation(Position3D);

            Matrix4 rotationX = Matrix4.CreateRotationX(Rotation3D.X);
            Matrix4 rotationY = Matrix4.CreateRotationY(Rotation3D.Y);
            Matrix4 rotationZ = Matrix4.CreateRotationZ(Rotation3D.Z);

            localMatrix.Value = (scaleMatrix * (rotationX * rotationY * rotationZ) * translation);
        }

        [BackgroundDependencyLoader]
        private void load(ShaderManager shaders, IRenderer renderer, TextureStore textureStore)
        {
        }
    }
}
