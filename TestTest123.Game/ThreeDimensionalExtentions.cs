using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Transforms;
using osuTK;

namespace TestTest123.Game
{
    public static class ThreeDimensionalExtentions
    {
        public static TransformSequence<T> MoveToOffset<T>(this T drawable, Vector3 offset, double duration = 0, Easing easing = Easing.None)
where T : ThreeDimensionalDrawable
        {
            return (drawable.TransformTo(nameof(drawable.Position3D), drawable.Position3D + offset, duration, easing));
        }
        public static TransformSequence<T> MoveTo<T>(this T drawable, Vector3 newPosition, double duration = 0, Easing easing = Easing.None)
    where T : ThreeDimensionalDrawable
        {
            return (drawable.TransformTo(nameof(drawable.Position3D), newPosition, duration, easing));
        }

        public static TransformSequence<T> RotateTo<T>(this T drawable, Vector3 newRotation, double duration = 0, Easing easing = Easing.None)
where T : ThreeDimensionalDrawable
        {
            return (drawable.TransformTo(nameof(drawable.Rotation3D), newRotation, duration, easing));
        }



    }
}
