using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Assimp;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics.Transforms;
using osu.Framework.Logging;
using osuTK;
using osuTK.Graphics.OpenGL;

namespace TestTest123.Game
{
    public partial class ThreeDimensionalDrawable : CompositeDrawable
    {
        public virtual Vector3 Forward {  get; set; }

        public Vector3 Scale3D;
        public virtual Vector3 Position3D {  get; set; }
        public Vector3 Rotation3D;

        public ThreeDimensionalDrawable()
        {
            RelativeSizeAxes = Axes.Both;
            Size = Vector2.One;
            Scale3D = Vector3.One;
            Position3D = Vector3.Zero;
            Rotation3D = Vector3.Zero;
        }

        public Matrix4 GetLocalMatrix()
        {
            Matrix4 scale = Matrix4.CreateScale(Scale3D);

            Matrix4 translation = Matrix4.CreateTranslation(Position3D);

            Matrix4 rotationX = Matrix4.CreateRotationX(Rotation3D.X);
            Matrix4 rotationY = Matrix4.CreateRotationY(Rotation3D.Y);
            Matrix4 rotationZ = Matrix4.CreateRotationZ(Rotation3D.Z);

            return (scale * (rotationX * rotationY * rotationZ) * translation);
        }

        [BackgroundDependencyLoader]
        private void load(ShaderManager shaders, IRenderer renderer, TextureStore textureStore)
        {
        }
    }
}
