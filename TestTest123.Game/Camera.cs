using osu.Framework.Allocation;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics;
using osuTK;
using osu.Framework.Input.Events;
using osu.Framework.Graphics.Sprites;
using System.Collections.Generic;
using osu.Framework.Logging;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Rendering;



namespace TestTest123.Game
{
    public partial class Camera : Model 
    {
        private Stage stage;
        private Vector3 position3D;
        private List<Vector3[]> image;


        private Matrix4 projectionMatrix;



        public float FarPlane{ get;}

        public Camera(Stage stage, Vector3 pos) : base(null)
        {
            stage.Add(this);
            this.stage = stage;
            position3D = pos;

            FarPlane = 5000f;

            RelativeSizeAxes = Axes.Both;
            RelativePositionAxes = Axes.Both;

            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;

            Size = new Vector2(0.8f, 0.8f);
            init();

        }

        private void init()
        {
            SetProjectionMatrix();
            image = stage.GetVertices(this);
        }

        
        public void SetProjectionMatrix()
        {
            projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(2, 16 / 9, 1, 5000);
        }

        public List<Vector3[]> Project()
        {
            List<Vector3[]> response = new List<Vector3[]>();
            image = stage.GetVertices(this);

            for (int i = 0; i < image.Count; i++) {

                if (image[i] == null) continue;
                Vector3[] temp = new Vector3[image[i].Length];

                for (int j = 0; j < image[i].Length; j++)
                {
                    temp[j] = Vector3.Project(position3D - image[i][j], 0, 0, DrawWidth, DrawHeight, 1, 5000, projectionMatrix);


                }
                response.Add(temp);
            }
            return (response);
        }


        public void MoveBy(Vector3 offset)
        {
            position3D += offset;
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

                Vector3[] vertices = camera.Project()[0];

                Quad top = new Quad(vertices[6].Xy, vertices[7].Xy, vertices[3].Xy, vertices[2].Xy);
                Quad bottom = new Quad(vertices[0].Xy, vertices[1].Xy, vertices[5].Xy, vertices[4].Xy);
                Quad front = new Quad(vertices[5].Xy, vertices[1].Xy, vertices[2].Xy, vertices[6].Xy);
                Quad back = new Quad(vertices[4].Xy, vertices[0].Xy, vertices[3].Xy, vertices[7].Xy);
                Quad left = new Quad(vertices[4].Xy, vertices[5].Xy, vertices[7].Xy, vertices[6].Xy);
                Quad right = new Quad(vertices[0].Xy, vertices[1].Xy, vertices[3].Xy, vertices[2].Xy); // Same order as top for a cube

                Quad[] quads = [top, bottom, front, back, left, right];

                for (int i = 0; i < quads.Length; i++)
                {
                    renderer.DrawQuad(renderer.WhitePixel, quads[i], DrawColourInfo.Colour);

                }

            }
        }
    }
}
