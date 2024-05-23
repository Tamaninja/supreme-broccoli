
using System;
using System.Collections.Generic;
using Assimp;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Textures;
using TestTest123.Game.Vertices;

namespace TestTest123.Game
{

    public partial class MeshDrawable : Component
    {
        public Material Material;
        public int[] Indices;
        public Vector3D[] Vertices;
        public List<Vector3D>[] TextureCoords;

        public MeshDrawable(Mesh mesh, Material material) {
            
            TextureCoords = mesh.TextureCoordinateChannels;
            Material = material;
            Indices = mesh.GetIndices();
            Vertices = mesh.Vertices.ToArray();
            
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textureStore)
        {
            Colour = Material.Colour;

        }
        public void Draw<T>(IVertexBatch<T> batch) where T :unmanaged, IMeshVertex<T>
        {

            Material.Texture?.Bind();

            for (int i = 0; i < Indices.Length; i++)
            {
                batch.AddAction(T.FromMesh(this, i));
            }

        }

    }
}
