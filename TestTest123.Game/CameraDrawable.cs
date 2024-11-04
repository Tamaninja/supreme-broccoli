using osu.Framework.Graphics;
using osuTK;
using osu.Framework.Input.Events;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Layout;
using osu.Framework.Logging;
using osu.Framework.Graphics.Sprites;
using osuTK.Graphics.OpenGL;
using osu.Framework.Bindables;
using Assimp;
using osuTK.Input;
using System;
using osu.Framework.Graphics.Shaders.Types;
using Vortice;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Pooling;
using osu.Framework.Graphics.Shapes;



namespace TestTest123.Game
{
    public partial class CameraDrawable : Container
    {
        private float pitch = 0;
        private float yaw = 0;
        private float speed = 25f;

        


        public Bindable<Matrix4> CameraViewProjection { get; } = new Bindable<Matrix4>(Matrix4.Identity);
        private readonly LayoutValue<Matrix4> projectionMatrix = new(Invalidation.MiscGeometry | Invalidation.DrawInfo | Invalidation.DrawSize);

        public virtual Matrix4 ProjectionMatrix => projectionMatrix.IsValid ? projectionMatrix : projectionMatrix.Value = createProjectionMatrix();


        public float VerticalFOV {  get; set; }
        public float FarPlane { get; set; }
        public float NearPlane { get; set; }

        public float AspectRatio { get; set; }
        public Scene Scene { get; set; }
        public Bindable<Matrix4> LocalMatrix { get; set; }
        public Bindable<Vector3> Position3D { get; set; } = new Bindable<Vector3>(Vector3.Zero);
        public Bindable<Vector3> Forward { get; set; } = new Bindable<Vector3>(Vector3.UnitZ);
        public virtual Vector3 Right => Vector3.Normalize(Vector3.Cross(Vector3.UnitY, Forward.Value));
        private SpriteText text;

        public Vector3 SetPosition
        {
            get => Position3D.Value;
            set => Position3D.Value = value;
        }
        public CameraDrawable(Scene scene, float verticalFOV, float aspectRatio, float nearPlane, float farPlane) {
            Scene = scene;
            VerticalFOV = verticalFOV;
            AspectRatio = aspectRatio;
            NearPlane = nearPlane;
            FarPlane = farPlane;

            RelativeSizeAxes = Axes.Both;

            Position3D.BindValueChanged((t) => UpdateMatrix());
            Forward.BindValueChanged((t) => UpdateMatrix());

            CameraViewProjection.BindValueChanged((t) => Invalidate(Invalidation.DrawNode));
            

            AddInternal(text = new SpriteText()
            {
                Text = ""
            });

            Position3D.BindValueChanged(t => text.Text = Position3D.ToString());
            Colour = Colour4.PaleGoldenrod;

        }
        public void UpdateMatrix()
        {
            Matrix4 viewMatrix = Matrix4.LookAt(Position3D.Value, Position3D.Value + Forward.Value, Vector3.UnitY);
            CameraViewProjection.Value = viewMatrix * ProjectionMatrix;            
        }
        private Matrix4 createProjectionMatrix()
        {

            Matrix4 matrix = Matrix4.CreatePerspectiveFieldOfView(
                MathHelper.DegreesToRadians(VerticalFOV), AspectRatio, NearPlane, FarPlane);
             
            return (matrix);
        }
        public Matrix4 LookAt(Vector3 position)
        {
            Matrix4 matrix = Matrix4.LookAt(Position3D.Value, position, Vector3.UnitY);

            Forward.Value = matrix.Column2.Xyz;
            return(matrix);
        }

        protected override bool OnMouseMove(MouseMoveEvent e)
        {
            Vector2 delta = e.Delta * 0.25f;
            yaw += delta.X;
            pitch = MathHelper.Clamp(pitch - delta.Y, -89, 89);
            Forward.Value = new Vector3
            {
                X = MathF.Cos(MathHelper.DegreesToRadians(yaw)) * MathF.Cos(MathHelper.DegreesToRadians(pitch)),
                Y = MathF.Sin(MathHelper.DegreesToRadians(pitch)),
                Z = MathF.Sin(MathHelper.DegreesToRadians(yaw)) * MathF.Cos(MathHelper.DegreesToRadians(pitch))
            };
            return base.OnMouseMove(e);
        }
        protected override bool OnKeyDown(KeyDownEvent e)
        {

            switch (e.Key)
            {

                case Key.Space:
                    this.MoveToOffset((Vector3.UnitY * speed),50);
                    break;

                case Key.ControlLeft:
                    this.MoveToOffset(-Vector3.UnitY * speed, 50);
                    break;

                case Key.A:
                    this.MoveToOffset(Right * speed, 50);
                    break;
                case Key.D:

                    this.MoveToOffset(-Right * speed, 50);
                    break;

                case Key.W:
                    this.MoveToOffset(Forward.Value * speed, 50);
                    break;
                case Key.S:
                    this.MoveToOffset(-Forward.Value * speed, 50);
                    break;
                
            }
            return base.OnKeyDown(e);
        }


        protected override DrawNode CreateDrawNode()
        {
            return new CameraDrawNode(this);
        }

        protected class CameraDrawNode : CompositeDrawableDrawNode
        {
            protected new CameraDrawable Source => (CameraDrawable)base.Source;
            private SceneNode sceneNode;
            private Matrix4 vpMatrix;

            public CameraDrawNode(CameraDrawable source) : base(source)
            {
                sceneNode = source.Scene.Node;
            }

            public override void ApplyState()
            {
                base.ApplyState();
                sceneNode = Source.Scene.Node;
                vpMatrix = Source.CameraViewProjection.Value;
            }

            protected override void Draw(IRenderer renderer)
            {
                base.Draw(renderer);

                renderer.PushDepthInfo(DepthInfo.Default);
                renderer.PushProjectionMatrix(vpMatrix);

                    sceneNode.CurrentShaderer.Draw(renderer);

                renderer.PopProjectionMatrix();
                renderer.PopDepthInfo();
            }
        }
    }
}
