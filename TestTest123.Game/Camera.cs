using osu.Framework.Allocation;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics;
using osuTK;
using osu.Framework.Input.Events;
using osu.Framework.Graphics.Sprites;
using System.Collections.Generic;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Rendering.Vertices;
using osu.Framework.Logging;
using System;
using osu.Framework.Graphics.Shaders;
using osuTK.Graphics;




namespace TestTest123.Game
{
    public partial class Camera : Model
    {
        private Stage stage;

        private Vector3 viewDirection;
        private Vector3 upAxis;
        private Vector3 rightAxis;
        private SpriteText spriteText;
        private Vector3 rotation;

        private float yFov = 60;
        public float FarPlane{ get;}

        public Camera(Stage stage, Vector3 pos, SpriteText debug) : base(pos)
        {
            stage.Add(this);
            this.stage = stage;

            FarPlane = 5000f;

            RelativeSizeAxes = Axes.Both;
            RelativePositionAxes = Axes.Both;

            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;

            spriteText = debug;
        }

        protected override void Init()
        {
            viewDirection = new Vector3(0, 0, -1);

    }

        public void MoveBy(Vector3 offset)
        {

        }


        public IReadOnlyList<Model> GetModels()
        {
            return (stage.Children);
        }

        public void UpdateVertexBuffer(Action<TexturedVertex3D> addAction, Vector3 vector)
        {
            Vector3 temp = Vector3.Project(GetPosition() - vector,0,0, DrawWidth, DrawHeight, 10, 5000, GetProjectionMatrix());

            addAction(new TexturedVertex3D()
            {
                Position = new Vector3(temp.X, -temp.Y, temp.Z),
                Colour = DrawColourInfo.Colour,
                TexturePosition = Vector2.Zero
            });
        }


        protected override bool OnMouseMove(MouseMoveEvent e)
        {
            Vector2 delta = e.Delta * 0.25f;

            rotation.X += delta.X;
            rotation.Y += -delta.Y;
            rotation.Y = MathHelper.Clamp(rotation.Y, -89, 89);

            viewDirection.X += MathF.Cos(MathHelper.DegreesToRadians(rotation.X)) * MathF.Cos(MathHelper.DegreesToRadians(rotation.Y));
            viewDirection.Y += MathF.Sin(MathHelper.DegreesToRadians(rotation.Y));
            viewDirection.Z += MathF.Sin(MathHelper.DegreesToRadians(rotation.X)) * MathF.Cos(MathHelper.DegreesToRadians(rotation.Y));

            viewDirection.Normalized();


            return base.OnMouseMove(e);
        }


        public Matrix4 GetViewMatrix()
        {
            Vector3 temp = Vector3.Normalize(Pos - viewDirection);
            rightAxis = Vector3.Cross(Vector3.UnitY, temp).Normalized();
            upAxis = Vector3.Cross(temp, rightAxis);

            return Matrix4.LookAt(Pos,Pos + viewDirection, Vector3.UnitY);
        }

        public Matrix4 GetProjectionMatrix()
        {

            return Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(yFov), 16 / 9, 5, 5000);
        }





        protected override bool OnKeyDown(KeyDownEvent e)
        {
            switch (e.Key)
            {

                case osuTK.Input.Key.Space:
                    SetPosition(GetPosition() + Vector3.UnitY);
                    return true;

                case osuTK.Input.Key.LShift:
                    SetPosition(GetPosition() - Vector3.UnitY);
                    return true;

                case osuTK.Input.Key.W:
                    MoveBy(new Vector3(0, 0, 1));
                    return true;

                case osuTK.Input.Key.S:
                    MoveBy(new Vector3(0, 0, -1));
                    return true;

                case osuTK.Input.Key.A:
                    MoveBy(new Vector3(1, 0, 0));
                    return true;

                case osuTK.Input.Key.D:
                    MoveBy(new Vector3(-1, 0, 0));
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

                foreach (Model model in camera.GetModels())
                {
                    IVertexBatch<TexturedVertex3D> batch = renderer.CreateQuadBatch<TexturedVertex3D>(6, 1);
                    Vector3[] vertices = model.GetVertices();
                    int[][] indices = model.GetIndices();

                    renderer.PushProjectionMatrix(model.GetMatrix() * camera.GetViewMatrix() * camera.GetProjectionMatrix());

                    for (int i = 0; i < indices.Length; i++)
                    {
                        for (int j = 0; j < indices[i].Length; j++)
                        {
                            camera.UpdateVertexBuffer(batch.AddAction, vertices[indices[i][j]]);
                        }
                    }
                    renderer.PopProjectionMatrix();

                }

                UnbindTextureShader(renderer);

            }
        }
    }
}
