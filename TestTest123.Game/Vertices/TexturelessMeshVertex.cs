
using System;
using System.Runtime.InteropServices;
using Assimp;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Rendering.Vertices;
using osuTK;
using osuTK.Graphics;
using osuTK.Graphics.ES30;

namespace TestTest123.Game.Vertices
{
    public struct TexturelessMeshVertex : IMeshVertex<TexturelessMeshVertex>
    {
        [VertexMember(3, VertexAttribPointerType.Float)]
        public Vector3D Position;

        [VertexMember(4, VertexAttribPointerType.Float)]
        public Color4 Colour;

        public static TexturelessMeshVertex FromMesh(Mesh<TexturelessMeshVertex> mesh, int index)
        {
            return new TexturelessMeshVertex
            {
                Position = mesh.Vertices[index],
                Colour = new Color4(1,0,1, 1),
            };
        }

        public readonly bool Equals(TexturelessMeshVertex other)
        {
            if (Position.Equals(other.Position))
            {
                return Colour.Equals(other.Colour);
            }

            return false;
        }
    }
}
