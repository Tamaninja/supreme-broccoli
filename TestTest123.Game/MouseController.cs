using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osu.Framework.Logging;
using osuTK;
using osuTK.Input;

namespace TestTest123.Game
{
    public partial class MouseController : Component
    {
        private Camera camera;
        private float sensitivity;
        private float pitch;
        private float yaw;

        public MouseController(Camera camera)
        {
            sensitivity = 0.25f;
            this.camera = camera;
        }



        protected override bool OnKeyDown(KeyDownEvent e)
        {
            switch (e.Key)
            {

                case Key.Space:
                    camera.MoveBy(new Vector3(0, 10, 0));
                    return true;

                case Key.LShift:
                    camera.MoveBy(new Vector3(0, -10, 0));
                    return true;

                case Key.A:
                    camera.MoveBy(new Vector3(-10f, 0, 0));
                    return true;

                case Key.D:
                    camera.MoveBy(new Vector3(10f, 0, 0));
                    return true;

                case Key.S:
                    camera.MoveBy(new Vector3(0, 0, 10f));
                    return true;

                case Key.W:
                    camera.MoveBy(new Vector3(0, 0, -10f));
                    return true;
            }


            return base.OnKeyDown(e);
        }
    }
}
