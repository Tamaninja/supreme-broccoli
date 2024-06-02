using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osu.Framework.Layout;
using osu.Framework.Logging;
using osuTK;
using osuTK.Input;

namespace TestTest123.Game
{
    public partial class MouseController : Container
    {
        private Camera camera;
        private float sensitivity = 0.25f;
        private float pitch = 0;
        private float yaw = 0;
        private float speed = 100;

        public MouseController(Camera camera)
        {
            this.camera = camera;
            Children = [
                new SpriteText()
                {
                    Text = camera.CameraViewProjection.ToString(),
                    Colour = Colour4.White,
                    AllowMultiline = true,
                    Depth = 0
                }
            ];
            RelativeSizeAxes = Axes.Both;
        }
        protected override bool OnMouseMove(MouseMoveEvent e)
        {
            Vector2 delta = e.Delta * sensitivity;
            yaw += delta.X;
            pitch = MathHelper.Clamp(pitch - delta.Y, -89, 89);
            camera.Forward = new Vector3
            {
                X = MathF.Cos(MathHelper.DegreesToRadians(yaw)) * MathF.Cos(MathHelper.DegreesToRadians(pitch)),
                Y = MathF.Sin(MathHelper.DegreesToRadians(pitch)),
                Z = MathF.Sin(MathHelper.DegreesToRadians(yaw)) * MathF.Cos(MathHelper.DegreesToRadians(pitch))
            };

            return base.OnMouseMove(e);
        }
        protected override bool OnKeyDown(KeyDownEvent e)
        {

            switch (e.Key)
            {

                case Key.Space:
                    camera.MoveToOffset(Vector3.UnitY, speed, Easing.None);
                    return true;

                case Key.LShift:
                    camera.MoveToOffset(-Vector3.UnitY, speed, Easing.None);
                    return true;

                case Key.A:
                    camera.MoveToOffset(camera.Right, speed, Easing.None);
                    return true;

                case Key.D:
                    camera.MoveToOffset(-camera.Right, speed, Easing.None);
                    return true;

                case Key.S:
                    camera.MoveToOffset(-camera.Forward, speed, Easing.None);
                    return true;

                case Key.W:
                    camera.MoveToOffset(camera.Forward, speed, Easing.None);
                    return true;


                default: return base.OnKeyDown(e);
            }
        }
    }
}
