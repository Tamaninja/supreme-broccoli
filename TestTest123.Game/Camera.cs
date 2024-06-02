using osu.Framework.Graphics;
using osuTK;
using osu.Framework.Input.Events;
using osu.Framework.Graphics.Rendering;
using JetBrains.Annotations;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Textures;
using osu.Framework.Layout;



namespace TestTest123.Game
{
    public partial class Camera : ThreeDimensionalDrawable
    {
        private readonly LayoutValue<Matrix4> projectionMatrix = new(Invalidation.MiscGeometry | Invalidation.DrawInfo);

        public virtual Matrix4 ProjectionMatrix => projectionMatrix.IsValid ? projectionMatrix : projectionMatrix.Value = createProjectionMatrix();






        public float VerticalFOV {  get; set; }
        public float FarPlane { get; set; }
        public float NearPlane { get; set; }

        public float AspectRatio { get; set; }




        


        public Camera(float verticalFOV, float aspectRatio, float nearPlane, float farPlane)
        {
            VerticalFOV = verticalFOV;
            AspectRatio = aspectRatio;
            NearPlane = nearPlane;
            FarPlane = farPlane;

            RelativeSizeAxes = Axes.Both;

        }
        public override void UpdateMatrix()
        {

            CameraViewProjection.Value = createViewMatrix() * ProjectionMatrix;
        }
        private Matrix4 createProjectionMatrix()
        {
            Matrix4 matrix = Matrix4.CreatePerspectiveFieldOfView(
                MathHelper.DegreesToRadians(VerticalFOV), AspectRatio, NearPlane, FarPlane);
            return (matrix);
        }

        private Matrix4 createViewMatrix()
        {
            Matrix4 matrix = Matrix4.LookAt(Position3D, Position3D + Forward, WORLD_UP);

            return (matrix);
        }

        [BackgroundDependencyLoader]
        private void load(ShaderManager shaders, IRenderer renderer, TextureStore textureStore)
        {
            AddInternal(new MouseController(this));
        }
    }
}
