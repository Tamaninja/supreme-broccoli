
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




namespace TestTest123.Game
{
    public partial class Camera : Model
    {
        private Stage stage;

        private float yaw;
        private float pitch;

        private Vector3 viewDirection;
        private SpriteText spriteText;

        private float yFov = 50;
        public float FarPlane { get; }

        public Camera(Stage stage, Vector3 pos, SpriteText debug) : base(pos)
        {
            stage.Add(this);
            this.stage = stage;

            FarPlane = 5000f;

            RelativeSizeAxes = Axes.Both;
            RelativePositionAxes = Axes.Both;

            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;

            AssimpContext importer = new AssimpContext();
            Scene test = importer.ImportFile("");

            test.Meshes[0].Vertices

        }

        protected override void Init()
        {
            viewDirection = new Vector3(0, 0, -1);
            pitch = 0;
            yaw = 0;
        }

        public void HandleMouseEvent(MouseMoveEvent e)
        {
            Vector2 delta = e.Delta * 0.25f;

            yaw += delta.X;
            pitch += -delta.Y;

            pitch = MathHelper.Clamp(pitch, -89, 89);

            UpdateViewDirection();
        }

        public void MoveBy(Vector3 offset)
        {

            

            Pos += Vector3.TransformNormal(offset, GetViewMatrix());
            
        }


        public IReadOnlyList<Model> GetModels()
        {
            return (stage.Children);
        }

        public void UpdateViewDirection()
        {


            viewDirection.X = MathF.Cos(MathHelper.DegreesToRadians(yaw)) * MathF.Cos(MathHelper.DegreesToRadians(pitch));
            viewDirection.Y = MathF.Sin(MathHelper.DegreesToRadians(pitch));
            viewDirection.Z = MathF.Sin(MathHelper.DegreesToRadians(yaw)) * MathF.Cos(MathHelper.DegreesToRadians(pitch));

            viewDirection.Normalize();
        }

        protected override bool OnMouseMove(MouseMoveEvent e)
        {
           HandleMouseEvent(e);
           

           return true;
        }


        public Matrix4 GetViewMatrix()
        {
            Vector3 temp = Vector3.Normalize(Pos - viewDirection);

            return Matrix4.LookAt(Pos, Pos + viewDirection, Vector3.UnitY);
        }

        public Matrix4 GetProjectionMatrix()
        {
            return Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(yFov), 16 / 9, 1, 50);
        }





        protected override bool OnKeyDown(KeyDownEvent e)
        {
            switch (e.Key)
            {

                case Key.Space:
                    SetPosition(GetPosition() + Vector3.UnitY);
                    return true;

                case Key.LShift:
                    SetPosition(GetPosition() - Vector3.UnitY);
                    return true;

                case Key.A:
                    MoveBy(new Vector3(1f, 0, 0));
                    return true;

                case Key.D:
                    MoveBy(new Vector3(-1f, 0, 0));
                    return true;

                case Key.S:
                    MoveBy(new Vector3(0, 0, -1f));
                    return true;

                case Key.W:
                    MoveBy(new Vector3(0, 0, 1f));
                    return true;
            }
            

            return base.OnKeyDown(e);
        }


        public override DrawColourInfo DrawColourInfo => new DrawColourInfo(Color4.White, base.DrawColourInfo.Blending);



        protected override DrawNode CreateDrawNode() => new CameraDrawNode(this);


        protected class CameraDrawNode : TexturedShaderDrawNode
        {
            private Camera camera;
            public CameraDrawNode(Camera source) : base(source)
            {
                camera = source;
                
            }

            protected override void Draw(IRenderer renderer)
            {

                BindTextureShader(renderer);
                renderer.PushProjectionMatrix(camera.GetViewMatrix() * camera.GetProjectionMatrix());


                foreach (Model model in camera.GetModels())
                {
                    Vector3[] vertices = model.GetVertices();
                    int[][] index = model.GetIndices();


                    renderer.PushLocalMatrix(model.GetMatrix());
                    IVertexBatch<TexturedVertex3D> batch = renderer.CreateQuadBatch<TexturedVertex3D>(6, 1);

                    for (int i = 0; i < index.Length; i++)
                    {
                        for (int j = 0; j < index[i].Length; j++)
                        {
                            batch.AddAction(new TexturedVertex3D()
                            {
                                Position = new Vector3(vertices[index[i][j]]),
                                Colour = DrawColourInfo.Colour,
                                TexturePosition = Vector2.Zero
                            });

                        }
                    }
                    renderer.PopLocalMatrix();
                }

                renderer.PopProjectionMatrix();
                UnbindTextureShader(renderer);

            }
        }
    }
}
