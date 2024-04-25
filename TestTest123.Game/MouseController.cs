using osu.Framework.Input.Events;
using osuTK;

namespace TestTest123.Game
{
    public partial class MouseController
    {
        private float sensitivity;
        public float Pitch { get;
            set 
                ; } = 0;
        public float Yaw
        {
            get => Yaw;
            set => MathHelper.Clamp(value, -89, 89);
        }

        public MouseController()
        {
            sensitivity = 0.25f;
        }

        public void HandleMouseEvent(MouseMoveEvent e)
        {
            Vector2 delta = e.Delta * sensitivity;

            Yaw += delta.X;
            Pitch += -delta.Y;
            Pitch = MathHelper.Clamp(Pitch, -89, 89);

        }

    }
}
