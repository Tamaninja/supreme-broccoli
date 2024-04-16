using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Primitives;
using osuTK;

namespace TestTest123.Game
{
    public partial class ZDrawable : Container
    {
        private Vector3 XYZ3D;

        private Camera camera;
        public ZDrawable(Camera camera, Vector3 xyz3d){
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            
            this.camera = camera;

            Set3DPos(xyz3d);
            RelativePositionAxes = Axes.Both;
        }

        public void AddQuad(Quad quad)
        {
            Add(new QuadSprite(quad));
        }



        public void Update3D()
        {
            
        }


        public float DistanceToCamera()
        {

            return (Vector3.Distance(XYZ3D, camera.XYZ3D));
        }

        public Vector3 Get3DPos()
        {
            return XYZ3D;
        }
        public void Set3DPos(Vector3 newPos)
        {
            //
            XYZ3D = newPos;
            Depth = (1 / newPos.Z);
            Update3D();
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
        }
    }
}
