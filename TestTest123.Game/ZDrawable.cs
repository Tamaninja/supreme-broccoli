using System;
using System.Threading;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osuTK;
using osuTK.Graphics;

namespace TestTest123.Game
{
    public partial class ZDrawable : Container
    {
        private Vector3 XYZ3D;

        private Camera camera;
        private SpriteText sprite;

        public ZDrawable(Camera camera, Vector3 xyz3D) {
            this.camera = camera;
            RelativePositionAxes = Axes.Both;

            sprite = new SpriteText()
            {
                Text = "test"
            };
            this.Add(sprite);
            Set3DPos(xyz3D);
        }

        public float DistanceToCamera()
        {
            return (Vector3.Distance(XYZ3D, camera.XYZ3D));
        }

        public bool IsVisible()
        {
            Vector3 diff = XYZ3D - camera.XYZ3D;
            sprite.Text = camera.FarPlane.ToString();
            if (camera.FarPlane > DistanceToCamera() && diff.Z > 0)
            {
                Vector3 visibleRange = camera.VisibleRange(diff.Z);
                if (visibleRange.X > Math.Abs(diff.X) && visibleRange.Y > Math.Abs(diff.Y))
                {
                    Colour = Color4.Green;
                    Position = camera.Vec3toVec2(diff);
                    return true;
                }
            }

            Colour = Color4.Red;
            return false;
        }

        public Vector3 Get3DPos()
        {
            return XYZ3D;
        }

        public void Update() {

            Scale = new Vector2(1 / (DistanceToCamera() + 1));
            IsVisible();

        }


        public void Set3DPos(Vector3 xyz3D)
        {
            //
            XYZ3D = xyz3D;
            Update();
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
        }

    }
}
