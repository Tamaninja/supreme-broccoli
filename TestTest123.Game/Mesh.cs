using System;
using System.Collections.Generic;
using Assimp;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Rendering.Vertices;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Logging;
using osuTK;
using osuTK.Graphics;
using osuTK.Graphics.OpenGL;
using TestTest123.Game.Vertices;
using Vortice;
using static osuTK.Graphics.OpenGL.GL;

namespace TestTest123.Game
{
    public record class Mesh
    {
        public int MaterialIndex;
        public int[] Indices;
        public string Name;
        public Vector3D[] Vertices;
        private IVertexBatch<TexturelessMeshVertex> defaultBatch; 
        public List<Vector3D>[] TextureCoords;
        public Model Parent { get; private set; }

        public Mesh(Model parent, Assimp.Mesh mesh){
            Parent = parent;
            Name = mesh.Name;
            MaterialIndex = mesh.MaterialIndex;
            Vertices = mesh.Vertices.ToArray();
            
            Indices = mesh.GetIndices();
            TextureCoords = mesh.TextureCoordinateChannels;

        }

        public void Stream(IRenderer renderer)
        {
            defaultBatch ??= renderer.CreateLinearBatch<TexturelessMeshVertex>(Indices.Length * 3, 3, PrimitiveTopology.Triangles);
            for (int i = 0; i < Indices.Length; i++)
            {
                defaultBatch.AddAction(TexturelessMeshVertex.FromMesh(this, Indices[i]));

            }
        }

        public void Stream<T>(IRenderer renderer, IVertexBatch<T> vertexBatch) where T : unmanaged, IMeshVertex<T>
        {
            for (int i = 0; i < Indices.Length; i++)
           {
              vertexBatch.AddAction(T.FromMesh(this, Indices[i]));
           }
        }
    }
}
