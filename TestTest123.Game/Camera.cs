using osu.Framework.Allocation;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics;
using osuTK;
using osu.Framework.Input.Events;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Logging;


namespace TestTest123.Game
{
    public partial class Camera : Container<ZDrawable>
    {
        protected Vector3 Pos3D;

        private Matrix4 projectionMatrix;
        private SpriteText output {  get; set; }

        public float FarPlane{ get;}

        public Camera(Vector3 pos, SpriteText output)
        {
            UpdateOrigin(pos);

            this.output = output;
            this.FarPlane = 5000f;

            RelativeSizeAxes = Axes.Both;
            RelativePositionAxes = Axes.Both;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            Size = new Vector2(0.8f, 0.8f);
        }

        public Matrix4 GetProjectionMatrix()
        {
            projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(2, 16 / 9, 1, 5000);
            return(projectionMatrix);

        }
        public void UpdateOrigin(Vector3 newPos)
        {
            Pos3D = newPos;

            
            foreach (ZDrawable child in Children)
            {


                child.ProjectVertices(GetProjectionMatrix(), this);
            }
        }

        public Vector3 GetPos3D()
        {
            return Pos3D;
        }

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            switch (e.Key)
            {

                case osuTK.Input.Key.Space:
                    UpdateOrigin(Pos3D + new Vector3(0, 1, 0));
                    return true;

                case osuTK.Input.Key.LShift:
                    UpdateOrigin(Pos3D + new Vector3(0, -1, 0));
                    return true;

                case osuTK.Input.Key.W:
                    UpdateOrigin(Pos3D + new Vector3(0, 0, 1));
                    return true;

                case osuTK.Input.Key.S:
                    UpdateOrigin(Pos3D + new Vector3(0, 0, -1));
                    return true;

                case osuTK.Input.Key.A:
                    UpdateOrigin(Pos3D + new Vector3(1, 0, 0));
                    return true;

                case osuTK.Input.Key.D:
                    UpdateOrigin(Pos3D + new Vector3(-1, 0, 0));
                    return true;
            }

            return base.OnKeyDown(e);
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            
        }
    }
}
