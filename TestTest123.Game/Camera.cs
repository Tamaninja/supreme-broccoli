
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
        public Vector3 Forward;
        private float yFov = 50;
        public float FarPlane { get; }
        public float NearPlane { get; }

        public float AspectRatio { get; }

        public Camera()
        {
            AspectRatio = 16 / 9;
            FarPlane = 5000f;
            NearPlane = 1f;
            Forward = Vector3.UnitZ;

            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.Both;
            RelativePositionAxes = Axes.Both;
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

        public void MoveBy(Vector3 offset)
        {
            Vector3 temp1 = (Matrix4.CreateTranslation(offset) * GetViewMatrix().Inverted()).ExtractTranslation();

            Position3D = temp1;
        }

        public Matrix4 GetProjectionMatrix()
        {
            return Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(yFov), AspectRatio, NearPlane, FarPlane);
        }

        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt(Position3D, Position3D + Forward, Vector3.UnitY);
        }

        [BackgroundDependencyLoader]
        private void load(ShaderManager shaders, IRenderer renderer, TextureStore textureStore)
        {
            AddInternal(new MouseController(this));
        }
    }
}
