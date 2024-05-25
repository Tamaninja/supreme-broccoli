﻿using System;
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
                    camera.MoveToOffset(Vector3.UnitY, 100, Easing.None);
                    return true;

                case Key.LShift:
                    camera.MoveToOffset(-Vector3.UnitY, 100, Easing.None);
                    return true;

                case Key.A:
                    return true;

                case Key.D:
                    return true;

                case Key.S:
                    camera.MoveToOffset(-camera.Forward, 100, Easing.None);
                    return true;

                case Key.W:
                    camera.MoveToOffset(camera.Forward, 100, Easing.None);
                    return true;
            }


            return base.OnKeyDown(e);
        }
    }
}
