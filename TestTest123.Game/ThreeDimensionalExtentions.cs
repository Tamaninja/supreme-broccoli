using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Transforms;
using osu.Framework.Localisation;
using osuTK;

namespace TestTest123.Game
{
    public static class ThreeDimensionalExtentions
    {
        public static TransformSequence<T> MoveToOffset<T>(this T drawable, Vector3 offset, double duration = 0, Easing easing = Easing.None)
where T : CameraDrawable
        {
            return drawable.TransformTo(nameof(CameraDrawable.SetPosition), drawable.Position3D.Value + offset, duration, easing);
        }
        public static TransformSequence<T> MoveToOffset<T>(this TransformSequence<T> t, Vector3 offset, double duration = 0, Easing easing = Easing.None)
where T : CameraDrawable
        {
            return t.Append((T o) => o.MoveToOffset(offset, duration, easing));
        }

        public static TransformSequence<T> MoveToZ<T>(this T drawable, float newZ, double duration = 0, Easing easing = Easing.None)
where T : CameraDrawable
        {
            return (drawable.TransformTo(nameof(CameraDrawable.SetPosition), new Vector3(drawable.Position3D.Value.X, drawable.Position3D.Value.Y, newZ), duration, easing));
        }
        public static TransformSequence<T> MoveToZ<T>(this TransformSequence<T> t, float newZ, double duration = 0, Easing easing = Easing.None)
where T : CameraDrawable
        {
            return t.Append((T o) => o.MoveToZ(newZ, duration, easing));
        }

        public static TransformSequence<T> MoveTo<T>(this T drawable, Vector3 newPosition, double duration = 0, Easing easing = Easing.None)
    where T : CameraDrawable
        {
            return (drawable.TransformTo(nameof(CameraDrawable.SetPosition), newPosition, duration, easing));
        }
        public static TransformSequence<T> MoveTo<T>(this TransformSequence<T> t, Vector3 newPosition, double duration = 0, Easing easing = Easing.None)
where T : CameraDrawable
        {

            return t.Append((T o) => o.MoveTo(newPosition, duration, easing));
        }


    }
}

