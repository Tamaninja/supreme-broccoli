
using osu.Framework.Graphics;
using osuTK;
using osu.Framework.Input.Events;
using osu.Framework.Graphics.Sprites;
using System.Collections.Generic;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Rendering.Vertices;
using System;
using osuTK.Graphics;
using osuTK.Input;
using Assimp;
using Assimp.Unmanaged;
using osu.Framework.Logging;
using osu.Framework.Graphics.Textures;
using osu.Framework.Platform;
using osu.Framework;



namespace TestTest123.Game
{
    public partial class Camera : Model
    {
        private Stage stage;

        private SpriteText spriteText;

        private float yFov = 50;
        public float FarPlane { get; }
        public float NearPlane { get; }

        public float AspectRatio { get; }

        public Camera(Stage stage, SpriteText debug)
        {
            AspectRatio = 16 / 9;
            stage.Add(this);
            this.stage = stage;
            spriteText = debug;
            FarPlane = 5000f;
            NearPlane = 1f;
            RelativeSizeAxes = Axes.Both;
            RelativePositionAxes = Axes.Both;

            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;

        }

        public void HandleMouseEvent(MouseMoveEvent e)
        {
            Vector2 delta = e.Delta * 0.25f;

            Yaw += delta.X;
            Pitch += -delta.Y;

            Pitch = MathHelper.Clamp(Pitch, -89, 89);
            spriteText.Text = (Forward.ToString());
            UpdateViewDirection();
        }
        public void MoveBy(Vector3 offset)
        {

            Vector3 temp1 = (Matrix4.CreateTranslation(offset) * GetViewMatrix().Inverted()).ExtractTranslation();

            Position = temp1;
        }


        public IReadOnlyList<Model> GetModels()
        {
            return (stage.Children);
        }

        public void UpdateViewDirection()
        {


            Forward.X = MathF.Cos(MathHelper.DegreesToRadians(Yaw)) * MathF.Cos(MathHelper.DegreesToRadians(Pitch));
            Forward.Y = MathF.Sin(MathHelper.DegreesToRadians(Pitch));
            Forward.Z = MathF.Sin(MathHelper.DegreesToRadians(Yaw)) * MathF.Cos(MathHelper.DegreesToRadians(Pitch));

        }

        protected override bool OnMouseMove(MouseMoveEvent e)
        {
            HandleMouseEvent(e);


            return true;
        }



        public Matrix4 GetProjectionMatrix()
        {
            return Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(yFov), AspectRatio, NearPlane, FarPlane);
        }





        protected override bool OnKeyDown(KeyDownEvent e)
        {
            switch (e.Key)
            {

                case Key.Space:
                    MoveBy(new Vector3(0, 100, 0));
                    return true;

                case Key.LShift:
                    MoveBy(new Vector3(0, -100, 0));
                    return true;

                case Key.A:
                    MoveBy(new Vector3(-100f, 0, 0));
                    return true;

                case Key.D:
                    MoveBy(new Vector3(100f, 0, 0));
                    return true;

                case Key.S:
                    MoveBy(new Vector3(0, 0, 100f));
                    return true;

                case Key.W:
                    MoveBy(new Vector3(0, 0, -100f));
                    return true;
            }


            return base.OnKeyDown(e);
        }

        protected override DrawNode CreateDrawNode() => new CameraDrawNode(this);


        protected class CameraDrawNode : TexturedShaderDrawNode
        {
            private Camera camera;
            public CameraDrawNode(Camera source) : base(source)
            {
                camera = source;

            }


            private void drawMeshes(Model model, IRenderer renderer)
            {

                renderer.BindTexture(model.Textures[0]);
                foreach (Mesh mesh in model.SceneInfo.Meshes)
                {
                    IVertexBatch<TexturedVertex3D> batch = renderer.CreateLinearBatch<TexturedVertex3D>(mesh.FaceCount * 4, mesh.FaceCount, PrimitiveTopology.Triangles);

                    int[] indices = mesh.GetIndices();
                    for (int i = 0; i < indices.Length; i++)
                    {
                        batch.AddAction(new TexturedVertex3D()
                        {
                            Position = mesh.Vertices[indices[i]],
                            Colour = new Color4D(1, 1, 1, 1),
                            TexturePosition = new Vector2D(mesh.TextureCoordinateChannels[0][indices[i]].X, mesh.TextureCoordinateChannels[0][indices[i]].Y)

                        }) ;

                    }
                }
            }
            protected override void Draw(IRenderer renderer)
            {
                
                BindTextureShader(renderer);
                renderer.PushProjectionMatrix(camera.GetViewMatrix() * camera.GetProjectionMatrix());

                foreach (Model model in camera.GetModels())
                {

                    if (model.SceneInfo == null) continue;



                    renderer.PushProjectionMatrix(model.GetLocalMatrix() * renderer.ProjectionMatrix);

                        drawMeshes(model, renderer);

                    renderer.PopProjectionMatrix();
                }

                renderer.PopProjectionMatrix();
                UnbindTextureShader(renderer);

            }
        }
    }
}
