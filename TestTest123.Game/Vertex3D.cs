
using System;
using System.Runtime.InteropServices;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Rendering.Vertices;
using osuTK;
using osuTK.Graphics;
using osuTK.Graphics.ES30;

namespace TestTest123.Game
{

    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex3D : IEquatable<Vertex3D>, IVertex
    {

        [VertexMember(3, VertexAttribPointerType.Float)]
        public Vector3 Position;

        [VertexMember(4, VertexAttribPointerType.Float)]
        public Color4 Color;


        [Obsolete("Initialise this type with an IRenderer instead", true)]
        public Vertex3D()
        {
            this = default; // explicitly initialise all members to default values
        }

        public Vertex3D(IRenderer renderer)
        {
            this = default; // explicitly initialise all members to default values


        }

        public bool Equals(Vertex3D other)
        {
            return(
                Position.Equals(other.Position)
                && Color.Equals(other.Color)
                );
        }
    }
}
