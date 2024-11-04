

using System.Runtime.InteropServices;
using Assimp;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shaders.Types;
using osuTK;

namespace TestTest123.Game
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public record struct UniformMaterial
    {
        public UniformVector4 Colour;

        public UniformMaterial(Color4D colour)
        {
            Colour.X = colour.R;
            Colour.Y = colour.G;
            Colour.Z = colour.B;
            Colour.W = colour.A;

        }
        public UniformMaterial(Colour4 colour)
        {
            Colour.X = colour.R;
            Colour.Y = colour.G;
            Colour.Z = colour.B;
            Colour.W = colour.A;
        }
    }
}
