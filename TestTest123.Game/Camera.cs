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




namespace TestTest123.Game
{
    public partial class Camera : Model
    {
        private Stage stage;

        private Matrix4 projectionMatrix;
        private Matrix4 viewMatrix;
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
            projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(0.87f, 16 / 9, 5, 500);

        }

       
        public IReadOnlyList<Model> GetModels()
        {
            return (stage.Children);
        }


        public Vector3[] Project(Vector3[] toProject)
        {

            viewMatrix = Matrix4.LookAt(GetPosition().Normalized(), GetViewDirection(), Vector3.UnitY);

            Vector3[] projection = new Vector3[toProject.Length];

            for (int i = 0; i < toProject.Length; i++)
            {
                projection[i] = Vector3.Project(GetPosition() - toProject[i], 0, 0, DrawWidth, DrawHeight, 5, 500,viewMatrix * projectionMatrix);
            }

            return projection;
        }

        protected override bool OnMouseMove(MouseMoveEvent e)
        {
            Vector2 delta = e.Delta * -0.25f;

            rotation.X += MathHelper.DegreesToRadians(delta.X);

            rotation.Y += MathHelper.DegreesToRadians(delta.Y);


            SetViewDirection(Vector3.UnitZ * (Matrix3.CreateRotationY(rotation.X) * Matrix3.CreateRotationX(rotation.Y)));


            Logger.Log(GetViewDirection().ToString());

            return base.OnMouseMove(e);
        }

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            switch (e.Key)
            {

                case osuTK.Input.Key.Space:
                    ClearRotation();
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

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            
        }

        protected override DrawNode CreateDrawNode() => new CameraDrawNode(this);



        protected class CameraDrawNode : SpriteDrawNode
        {
            private Camera camera;
            public CameraDrawNode(Camera source) : base(source)
            {
                camera = source;
            }

            protected override void Draw(IRenderer renderer)
            {
                base.Draw(renderer);

                foreach (Model model in camera.GetModels())
                {
                    Vector3[] vertices = camera.Project(model.GetVertices());
                    


                    int[][] indices = model.GetIndices();

                    IVertexBatch<TexturedVertex2D> batch = renderer.CreateQuadBatch<TexturedVertex2D>(6, 1);

                    for (int i = 0; i < indices.Length; i++)
                    {
                        for (int j = 0; j < indices[i].Length; j++)
                        {
                            batch.Add(new TexturedVertex2D(renderer)
                            {
                                Position = vertices[indices[i][j]].Xy,
                                TexturePosition = Vector2.Zero,
                                TextureRect = Vector4.Zero,
                                Colour = model.DrawColourInfo.Colour,
                            });
                        }
                    }


                }

            }
        }
    }
}
