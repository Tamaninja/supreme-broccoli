
using Assimp;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Pooling;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Textures;
using osu.Framework.Layout;
using osu.Framework.Logging;
using osu.Framework.Utils;
using osuTK;
using osuTK.Graphics.OpenGL;
using TestTest123.Game.Material;

namespace TestTest123.Game
{
    public interface IThreeDimensional
    {

        public static Vector3 WORLD_FORWARD = Vector3.UnitZ;
        public static Vector3 WORLD_UP = Vector3.UnitY;

        public Bindable<Matrix4> LocalMatrix {  get; set; }
                
        public virtual Vector3 Right => Vector3.Normalize(Vector3.Cross(WORLD_UP, Forward.Value));
        public Vector3 Up => Vector3.Cross(Forward.Value, Right);
        public Bindable<Vector3> Forward { get; set; }
        public Bindable<Vector3> Rotation { get; set; }
        public Bindable<Vector3> Position { get; set; }
        public Bindable<Vector3> Scale { get; set; }





    }
}
