using osu.Framework.Graphics;
using osuTK;
using osu.Framework.Input.Events;
using osu.Framework.Graphics.Rendering;
using JetBrains.Annotations;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Textures;
using System;
using osu.Framework.Bindables;
using Assimp;
using osu.Framework.Layout;
using osu.Framework.Logging;



namespace TestTest123.Game
{
    public partial class Camera : ThreeDimensionalDrawable
    {
        private Matrix4 projectionMatrix;
        private Matrix4 viewMatrix;



        public float VerticalFOV {  get; set; }
        public float FarPlane { get; set; }
        public float NearPlane { get; set; }

        public float AspectRatio { get; set; }

        public Vector3 Right => Vector3.Normalize(Vector3.Cross(WORLD_UP, Forward));
        public Vector3 Up => Vector3.Cross(Forward, Right);


        


        public Camera(float verticalFOV, float aspectRatio, float nearPlane, float farPlane)
        {
            VerticalFOV = verticalFOV;
            AspectRatio = aspectRatio;
            NearPlane = nearPlane;
            FarPlane = farPlane;

            projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(VerticalFOV), AspectRatio, NearPlane, FarPlane);


        }

        protected override bool OnMouseMove(MouseMoveEvent e)
        {

            Vector2 delta = e.Delta * 0.25f;
            Rotation3D = new Vector3(Rotation3D.X + delta.X, MathHelper.Clamp(Rotation3D.Y - delta.Y, -89, 89), 0);
            Forward = new Vector3
            {
                X = MathF.Cos(MathHelper.DegreesToRadians(Rotation3D.X)) * MathF.Cos(MathHelper.DegreesToRadians(Rotation3D.Y)),
                Y = MathF.Sin(MathHelper.DegreesToRadians(Rotation3D.Y)),
                Z = MathF.Sin(MathHelper.DegreesToRadians(Rotation3D.X)) * MathF.Cos(MathHelper.DegreesToRadians(Rotation3D.Y))
            };

            return base.OnMouseMove(e);
        }

        
        public override void UpdateMatrix()
        {
            
            viewMatrix = Matrix4.LookAt(Position3D, Position3D + Forward, WORLD_UP);
            CameraViewProjection.Value = viewMatrix * projectionMatrix;

        }

        [BackgroundDependencyLoader]
        private void load(ShaderManager shaders, IRenderer renderer, TextureStore textureStore)
        {
            AddInternal(new MouseController(this));
        }
    }
}
