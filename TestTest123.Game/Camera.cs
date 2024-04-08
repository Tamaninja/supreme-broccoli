using osu.Framework.Allocation;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osuTK;
using osu.Framework.Input.Events;
using osu.Framework.Graphics.Sprites;
using System;


namespace TestTest123.Game
{
    public partial class Camera : Container<ZDrawable>
    {
        public Vector3 XYZ3D {  get; set; }
        private SpriteText output {  get; set; }

        private int hFov = 90;
        private int vFov = 60;

        public float FarPlane{ get;}
        private double hRange;
        private double vRange;

        public Camera(Vector3 xyz3D, SpriteText output)
        {
            this.output = output;
            this.FarPlane = 5000f;
            initViewport();
            Set3DPos(xyz3D);


            RelativeSizeAxes = Axes.Both;
            RelativePositionAxes = Axes.Both;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            Size = new Vector2(0.8f, 0.8f);

        }

        public Vector3 VisibleRange(float distance)
        {

            float distanceSqrd = distance * distance;

            float hLimit = MathF.Sqrt(distanceSqrd + distanceSqrd - 2 * distanceSqrd * MathF.Cos(hFov)) / 2;
            float vLimit = MathF.Sqrt(distanceSqrd + distanceSqrd - 2 * distanceSqrd * MathF.Cos(vFov)) / 2;

            Vector3 visibleRange = new Vector3(hLimit, vLimit, distance);
            output.Text = visibleRange.ToString();  
            return (visibleRange);
        }

        public Vector2 Vec3toVec2(Vector3 diff)
        {
            Vector3 visibleRange = VisibleRange(diff.Z);

            return (new Vector2(1/visibleRange.X*diff.X, 1/visibleRange.Y*diff.Y));
        }
        private void initViewport()
        {
            float farPlaneSqrd = FarPlane * FarPlane;
            hRange = Math.Sqrt(farPlaneSqrd + farPlaneSqrd - 2 * farPlaneSqrd * Math.Cos(hFov));
            vRange = Math.Sqrt(farPlaneSqrd + farPlaneSqrd - 2 * farPlaneSqrd * Math.Cos(vFov));
        }
        public void Set3DPos(Vector3 xyz3D)
        {
            XYZ3D = xyz3D;
            output.Text = xyz3D.ToString() +" "+ hRange +" "+ vRange;



            foreach (ZDrawable drawable in InternalChildren)
            {
                drawable.Update();
            }
        }   

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            switch (e.Key)
            {

                case osuTK.Input.Key.Space:
                    Set3DPos(XYZ3D + new Vector3(0, 1, 0));
                    return true;

                case osuTK.Input.Key.LShift:
                    Set3DPos(XYZ3D + new Vector3(0, -1, 0));
                    return true;

                case osuTK.Input.Key.W:
                    Set3DPos(XYZ3D + new Vector3(0, 0, 1));
                    return true;

                case osuTK.Input.Key.S:
                    Set3DPos(XYZ3D + new Vector3(0, 0, -1));
                    return true;

                case osuTK.Input.Key.A:
                    Set3DPos(XYZ3D + new Vector3(-1, 0, 0));
                    return true;

                case osuTK.Input.Key.D:
                    Set3DPos(XYZ3D + new Vector3(1, 0, 0));
                    return true;
            }

            return base.OnKeyDown(e);
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
        }
    }
}
