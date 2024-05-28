using System.Collections.Generic;
using Assimp;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Rendering.Vertices;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Logging;
using osuTK;
using osuTK.Graphics;
using TestTest123.Game.Vertices;
using Vortice;

namespace TestTest123.Game
{
    public partial class Mesh<T> where T : unmanaged, IMeshVertex<T>
    {

        private IVertexBatch<T> vertexBatch;

        public Material Material;
        public int[] Indices;
        public Vector3D[] Vertices;
        public List<Vector3D>[] TextureCoords;

        
        public Mesh(Assimp.Mesh assimpMesh, Material material){
            Material = material;
            TextureCoords = assimpMesh.TextureCoordinateChannels;
            Indices = assimpMesh.GetIndices();
            Vertices = assimpMesh.Vertices.ToArray();
        }

        public void Draw(IRenderer renderer)
        {
            

            vertexBatch ??= renderer.CreateLinearBatch<T>(Indices.Length * 3, 3, PrimitiveTopology.Triangles);
            for (int i = 0; i < Indices.Length; i++)
            {
                vertexBatch.AddAction(T.FromMesh(this, Indices[i]));
            }
        }
    }
}
