

using System.Runtime.InteropServices;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shaders.Types;

namespace TestTest123.Game.Material
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public record struct UniformMaterial
    {
        public UniformVector4 Colour;

    }
}
