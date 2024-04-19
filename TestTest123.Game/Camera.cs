using osu.Framework.Allocation;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics;
using osuTK;
using osu.Framework.Input.Events;
using osu.Framework.Graphics.Sprites;
using System.Collections.Generic;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Rendering.Vertices;
using System.Linq;



namespace TestTest123.Game
{
    public partial class Camera : Model
    {
        private Stage stage;
        private List<Vector3[]> image;


        private Matrix4 projectionMatrix;

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
            SetProjectionMatrix();
        }

        
        public void SetProjectionMatrix()
        {
            projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(2, 16 / 9, 1, 5000);
        }

        public IReadOnlyList<Model> GetModels()
        {
            return (stage.Children);
        }

        public Vector3[] Project(Vector3[] toProject)
        {
            Vector3[] projection = new Vector3[toProject.Length];

            for (int i = 0; i < toProject.Length; i++)
            {
                projection[i] = Vector3.Project(Pos3D - toProject[i], 0, 0, DrawWidth, DrawHeight, 1, 5000, projectionMatrix);
            }

            return projection;
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
                    Vector3[] vertices = model.GetVertices();
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
                                Colour = DrawColourInfo.Colour,
                            });
                        }
                    }


                }

            }
        }
    }
}
