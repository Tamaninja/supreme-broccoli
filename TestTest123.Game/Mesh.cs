using System;
using System.Collections.Generic;
using Assimp;
using osu.Framework.Graphics.Rendering;
using osu.Framework.IO.Stores;
using TestTest123.Game.Vertices;

namespace TestTest123.Game
{
    public record class Mesh
    {
        public int MaterialIndex;
        public int[] Indices;
        public string Name;
        public Vector3D[] Vertices;
        public List<Vector3D>[] TextureCoords;
        private IVertexBatch<TexturedMeshVertex> vertexBatch;
        public Model Parent { get; private set; }

        public Mesh(Model parent, Assimp.Mesh mesh){
            Parent = parent;
            Name = mesh.Name;
            MaterialIndex = mesh.MaterialIndex;
            Vertices = mesh.Vertices.ToArray();
            
            Indices = mesh.GetIndices();
            TextureCoords = mesh.TextureCoordinateChannels;
        }
        public void Draw(IRenderer renderer)
        {
            vertexBatch ??= renderer.CreateLinearBatch<TexturedMeshVertex>(Indices.Length * 3, 3, PrimitiveTopology.Triangles);

           for (int i = 0; i < Indices.Length; i++)
           {
              vertexBatch.AddAction(TexturedMeshVertex.FromMesh(this, Indices[i]));
           }
        }
    }
}
