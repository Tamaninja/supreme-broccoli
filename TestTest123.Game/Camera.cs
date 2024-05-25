
using osu.Framework.Graphics;
using osuTK;
using osu.Framework.Input.Events;
using osu.Framework.Graphics.Sprites;
using System.Collections.Generic;
using osu.Framework.Graphics.Rendering;
using System;
using osu.Framework.Logging;
using Assimp;
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
        private bool recalcMatrix;
        public virtual Matrix4 ProjectionMatrix
        {
            get
            {
                if (recalcMatrix) recalculateMatrix();
                return projectionMatrix;
            }
        }
        public virtual Matrix4 PVMatrix
        {
            get
            {
                if (recalcMatrix) recalculateMatrix();
                return (pvMatrix);
            }
        }
        public virtual Matrix4 ViewMatrix
        {
            get
            {
                if (recalcMatrix) recalculateMatrix();
                return viewMatrix;
            }
        }
        private float yFov;
        public float FarPlane { get; }
        public float NearPlane { get; }

        public float AspectRatio { get; }

        public Camera()
        {
            recalcMatrix = true;
            yFov = 50;
            AspectRatio = 16 / 9;
            FarPlane = 5000f;
            NearPlane = 1f;
            Forward = Vector3.UnitZ;

            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.Both;
            RelativePositionAxes = Axes.Both;
            
        }

        public override Vector3 Forward {
            get => base.Forward;
            set {
                base.Forward = value;
                recalcMatrix = true;
            }
        }
        public override Vector3 Position3D {
            get => base.Position3D;
            set
            {
                base.Position3D = value;
                recalcMatrix = true;
            }
        }

        protected override bool OnMouseMove(MouseMoveEvent e)
        {
            Vector2 delta = e.Delta * 0.25f;

            Rotation3D.X += delta.X;
            Rotation3D.Y += -delta.Y;

            Rotation3D.Y = MathHelper.Clamp(Rotation3D.Y, -89, 89);

            Forward.X = MathF.Cos(MathHelper.DegreesToRadians(Rotation3D.X)) * MathF.Cos(MathHelper.DegreesToRadians(Rotation3D.Y));
            Forward.Y = MathF.Sin(MathHelper.DegreesToRadians(Rotation3D.Y));
            Forward.Z = MathF.Sin(MathHelper.DegreesToRadians(Rotation3D.X)) * MathF.Cos(MathHelper.DegreesToRadians(Rotation3D.Y));

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
