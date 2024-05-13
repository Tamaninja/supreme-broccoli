
using System;
using System.Runtime.InteropServices;
using Assimp;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Rendering.Vertices;
using osuTK;
using osuTK.Graphics;
using osuTK.Graphics.ES30;

namespace TestTest123.Game
{

    public struct TexturedVertex3D : IEquatable<TexturedVertex3D>, IVertex
    {
        [VertexMember(3, VertexAttribPointerType.Float)]
        public Vector3D Position;

        [VertexMember(4, VertexAttribPointerType.Float)]
        public Color4D Colour;

        [VertexMember(2, VertexAttribPointerType.Float)]
        public Vector2D TexturePosition;
        
        public readonly bool Equals(TexturedVertex3D other)
        {
            if (Position.Equals(other.Position) && TexturePosition.Equals(other.TexturePosition))
            {
                return Colour.Equals(other.Colour);
            }

            return false;
        }
    }
}
