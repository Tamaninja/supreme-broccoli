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




namespace TestTest123.Game
{
    public partial class Camera : Model
    {
        private Stage stage;

        private Vector3 viewDirection;
        private Vector3 upAxis;
        private Vector3 rightAxis;
        public float FarPlane{ get;}

        public Camera(Stage stage, Vector3 pos) : base(pos)
        {
            stage.Add(this);
            this.stage = stage;

            FarPlane = 5000f;

            RelativeSizeAxes = Axes.Both;
            RelativePositionAxes = Axes.Both;

            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            Size = new Vector2(0.8f, 0.8f);
        }

        
        
        
        protected override void Init()
        {
            viewDirection = new Vector3(0, 0, -1);

    }



        public IReadOnlyList<Model> GetModels()
        {
            return (stage.Children);
        }

        public void UpdateVertexBuffer(Action<TexturedVertex3D> addAction, Vector3 vector)
        {
            Vector3 temp = Vector3.TransformPerspective(vector, GetProjectionMatrix());
            Logger.LogPrint(temp.ToString());

            addAction(new TexturedVertex3D()
            {
                Position = new Vector3(temp.X, temp.Y, temp.Z),
                Colour = DrawColourInfo.Colour,
                TexturePosition = Vector2.Zero
            });
        }

        protected override bool OnMouseMove(MouseMoveEvent e)
        {
            Vector2 delta = e.Delta * -0.25f;

            rotation.X += delta.X;
            rotation.Y += delta.Y;

            viewDirection.X += MathF.Cos(MathHelper.DegreesToRadians(rotation.X)) * MathF.Cos(MathHelper.DegreesToRadians(rotation.Y));
            viewDirection.Y += MathF.Sin(MathHelper.DegreesToRadians(rotation.Y));
            viewDirection.Z += MathF.Sin(MathHelper.DegreesToRadians(rotation.X)) * MathF.Cos(MathHelper.DegreesToRadians(rotation.Y));
            viewDirection.Normalize();



            return base.OnMouseMove(e);
        }



        public Matrix4 GetViewMatrix()
        {
            Vector3 temp = Vector3.Normalize(Pos - viewDirection);
            rightAxis = Vector3.Cross(Vector3.UnitY, temp).Normalized();
            upAxis = Vector3.Cross(temp, rightAxis);

            return Matrix4.LookAt(Pos,Vector3.Zero, Vector3.UnitY);
        }

        public Matrix4 GetProjectionMatrix()
        {

            return Matrix4.CreatePerspectiveFieldOfView(0.87f, 16 / 9, 5, 500);
        }


        public void MoveBy(Vector3 offset)
        {

            
            SetPosition(Pos + offset);

        }

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            switch (e.Key)
            {

                case osuTK.Input.Key.Space:
                    MoveBy(new Vector3(0, 1, 0));
                    return true;

                case osuTK.Input.Key.LShift:
                    MoveBy(new Vector3(0, -1, 0));
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
                base.Draw(renderer);

                BindTextureShader(renderer);

                foreach (Model model in camera.GetModels())
                {
                    IVertexBatch<TexturedVertex3D> batch = renderer.CreateQuadBatch<TexturedVertex3D>(6, 1);
                    renderer.PushProjectionMatrix(camera.GetProjectionMatrix());
                    renderer.PushDepthInfo(DepthInfo.Default);


                    Vector3[] vertices = model.GetVertices();
                    int[][] indices = model.GetIndices();

                    for (int i = 0; i < indices.Length; i++)
                    {
                        for (int j = 0; j < indices[i].Length; j++)
                        {

                            camera.UpdateVertexBuffer(batch.AddAction, vertices[indices[i][j]]);


                        }
                    }


                    renderer.PopDepthInfo();
                    renderer.PopProjectionMatrix();
                }

                UnbindTextureShader(renderer);

            }
        }
    }
}
