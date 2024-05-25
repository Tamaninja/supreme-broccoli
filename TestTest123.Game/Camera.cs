using osu.Framework.Graphics;
using osuTK;
using osu.Framework.Input.Events;
using osu.Framework.Graphics.Rendering;
using System;
using JetBrains.Annotations;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Textures;



namespace TestTest123.Game
{
    public partial class Camera : ThreeDimensionalDrawable
    {
        private Matrix4 projectionMatrix;
        private Matrix4 viewMatrix;
        private Matrix4 pvMatrix;

        public virtual Matrix4 ProjectionMatrix
        {
            get
            {
                if (RequiresRecalculation) recalculateMatrix();
                return projectionMatrix;
            }
        }

        public virtual Matrix4 PVMatrix
        {
            get
            {
                if (RequiresRecalculation) recalculateMatrix();
                return (pvMatrix);
            }
        }

        public virtual Matrix4 ViewMatrix
        {
            get
            {
                if (RequiresRecalculation) recalculateMatrix();
                return viewMatrix;
            }
        }

        private float yFov;
        public float FarPlane { get; }
        public float NearPlane { get; }

        public float AspectRatio { get; }

        public Camera()
        {
            yFov = 50;
            AspectRatio = 16 / 9;
            FarPlane = 5000f;
            NearPlane = 1f;

            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.Both;
            RelativePositionAxes = Axes.Both;
        }

        protected override bool OnMouseMove(MouseMoveEvent e)
        {
            Vector2 delta = e.Delta * 0.25f;
            Rotation3D = new Vector3(Rotation3D.X + delta.X, MathHelper.Clamp(Rotation3D.Y - delta.Y, -89, 89), 0);
            Vector3 forward = new Vector3
            {
                X = MathF.Cos(MathHelper.DegreesToRadians(Rotation3D.X)) * MathF.Cos(MathHelper.DegreesToRadians(Rotation3D.Y)),
                Y = MathF.Sin(MathHelper.DegreesToRadians(Rotation3D.Y)),
                Z = MathF.Sin(MathHelper.DegreesToRadians(Rotation3D.X)) * MathF.Cos(MathHelper.DegreesToRadians(Rotation3D.Y))
            };
            Forward = forward;
            return (base.OnMouseMove(e));
        }

        private void recalculateMatrix()
        {
            projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(yFov), AspectRatio, NearPlane, FarPlane);
            viewMatrix = Matrix4.LookAt(Position3D, Position3D + Forward, Vector3.UnitY);
            pvMatrix = viewMatrix * projectionMatrix;
        }

        [BackgroundDependencyLoader]
        private void load(ShaderManager shaders, IRenderer renderer, TextureStore textureStore)
        {
            AddInternal(new MouseController(this));
        }
    }
}
