using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagedBass.Fx;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Input;

namespace TestTest123.Game
{
    public class MouseController
    {
        private bool snapToCenter;
        public float Pitch { get; set; } = 0;
        public float Yaw
        {
            get => Yaw;
            set => MathHelper.Clamp(value, -89, 89);
        }

        public MouseController(bool snapToCenter)
        {
            this.snapToCenter = snapToCenter;
        }



    }
}
