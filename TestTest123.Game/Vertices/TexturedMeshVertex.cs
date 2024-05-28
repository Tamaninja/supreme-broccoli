using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assimp;
using osu.Framework.Graphics.Rendering.Vertices;
using osuTK.Graphics;
using osuTK.Graphics.ES30;

namespace TestTest123.Game.Vertices
{
    public struct TexturedMeshVertex : IMeshVertex<TexturedMeshVertex>
    {
        [VertexMember(3, VertexAttribPointerType.Float)]
        public Vector3D Position;

        [VertexMember(4, VertexAttribPointerType.Float)]
        public Color4 Colour;

        [VertexMember(3, VertexAttribPointerType.Float)]
        public Vector3D TexturePosition;

        public static TexturedMeshVertex FromMesh(Mesh<TexturedMeshVertex> mesh, int index)
        {
            return (new TexturedMeshVertex()
            {
                Position = mesh.Vertices[index],
                Colour = new Color4(1, 0, 1, 1),
                TexturePosition = mesh.TextureCoords[0][index]
            });
        }

        public bool Equals(TexturedMeshVertex other)
        {
            if (Position.Equals(other.Position))
            {
                if (TexturePosition.Equals(other.TexturePosition))
                {
                    return Colour.Equals(other.Colour);
                }
            }

            return false;
        }
    }
}
