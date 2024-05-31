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

        private IVertexBatch<TexturedMeshVertex> vertexBatch {  get; set; }
        public int MaterialIndex;
        public int[] Indices;
        public string Name;
        public Vector3D[] Vertices;
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

        public void DrawVBO(IRenderer renderer)
        {
            vertexBatch ??= renderer.CreateLinearBatch<TexturedMeshVertex>(Indices.Length * 3, 3, PrimitiveTopology.Triangles);

            for (int i = 0; i < Indices.Length; i++)
            {
                vertexBatch.AddAction(TexturedMeshVertex.FromMesh(this, i));
            }
        }

    }
}
