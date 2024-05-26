﻿using System.Collections.Generic;
using Assimp;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Textures;
using osu.Framework.Logging;
using osuTK.Graphics;
using TestTest123.Game.Vertices;

namespace TestTest123.Game
{
    public partial class MeshDrawable : Component
    {
        public Material Material;
        public int[] Indices;
        public Vector3D[] Vertices;
        public List<Vector3D>[] TextureCoords;

        public MeshDrawable(Mesh mesh, Material material)
        {
            Colour = Color4.Green;
            TextureCoords = mesh.TextureCoordinateChannels;
            Material = material;
            Indices = mesh.GetIndices();
            Vertices = mesh.Vertices.ToArray();
            Name = mesh.Name;
        }

        protected override DrawNode CreateDrawNode()
        {
            return (new MeshDrawNode<TexturelessMeshVertex>(this));
        }

        protected class MeshDrawNode<T>(MeshDrawable source) : DrawNode(source)
            where T : unmanaged, IMeshVertex<T>
        {
            private IVertexBatch<T> vertexBatch;

            private MeshDrawable mesh = source;

            protected override void Draw(IRenderer renderer)
            {
                vertexBatch ??= renderer.CreateLinearBatch<T>(mesh.Indices.Length * 3, 3, PrimitiveTopology.Triangles);
                mesh.Material.Texture?.Bind();

                for (int i = 0; i < mesh.Indices.Length; i++)
                {
                    vertexBatch.AddAction(T.FromMesh(mesh, mesh.Indices[i]));
                }
            }
        }
    }
}
