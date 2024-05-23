
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

        public static TexturelessMeshVertex FromMesh(MeshDrawable mesh, int index)
        {
            return new TexturelessMeshVertex
            {
                Colour = mesh.Colour,
                Position = mesh.Vertices[index]
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
