using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using osu.Framework.Graphics.Shaders.Types;

namespace TestTest123.Game
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public record struct UniformMaterial
    {
        public UniformVector4 Colour;
    }
}
