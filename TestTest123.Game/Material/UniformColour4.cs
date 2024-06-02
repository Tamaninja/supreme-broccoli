

using System.Runtime.InteropServices;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shaders.Types;
using osuTK.Graphics;

namespace TestTest123.Game
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 16)]
    public record struct UniformColour4
    {
        public UniformFloat R;
        public UniformFloat G;
        public UniformFloat B;
        public UniformFloat A;

        public static implicit operator Colour4(UniformColour4 value) => new Colour4(value.R, value.G, value.B, value.A);

        public static implicit operator UniformColour4(Colour4 value) => new UniformColour4
        {
            R = value.R,
            G = value.G,
            B = value.B,
            A = value.A,
        };

        public static implicit operator Color4(UniformColour4 value) => new Color4(value.R, value.G, value.B, value.A);
        public static implicit operator UniformColour4(Color4 value) => new UniformColour4
        {
            R = value.R,
            G = value.G,
            B = value.B,
            A = value.A,
        };
    }

}
