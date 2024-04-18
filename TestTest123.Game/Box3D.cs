using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osuTK;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Primitives;
using osuTK.Graphics;
using osu.Framework.Extensions.Color4Extensions;

namespace TestTest123.Game
{
    public partial class Box3D : Model
    {
        public Box3D(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4, Vector3 v5, Vector3 v6, Vector3 v7, Vector3 v8)
            : base([v1, v2, v3, v4, v5, v6, v7, v8])
        {

            Colour = Color4.AliceBlue.Opacity(0.5f);
        }


    }
}
