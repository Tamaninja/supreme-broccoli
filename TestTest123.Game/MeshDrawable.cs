
using System;
using Assimp;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Textures;
using osu.Framework.Logging;
using osuTK;

namespace TestTest123.Game
{

    public partial class MeshDrawable : Component
    {
        public Material Material;
        public int[] Indices;
        public Vector3D[] Vertices;

        public MeshDrawable(Mesh mesh, Material material) {


            Material = material;
            Indices = mesh.GetIndices();
            Vertices = mesh.Vertices.ToArray();
        }

        public void Draw(Action<TexturelessVertex3D> add)
        {

            Material.Texture?.Bind();

            for (int i = 0; i < Indices.Length; i++)
            {
                add(new TexturelessVertex3D()
                {
                    Position = Vertices[Indices[i]],
                    Colour = Material.ColorDiffuse,
/*                    TexturePosition = mesh.TextureCoordinateChannels[0][indices[i]]
*/
                });

            }
        }


    }
}
