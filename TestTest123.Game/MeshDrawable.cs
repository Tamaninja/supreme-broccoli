using System.Collections.Generic;
using Assimp;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
<<<<<<< HEAD
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Rendering.Vertices;
using osu.Framework.Graphics.Sprites;
=======
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Rendering.Vertices;
>>>>>>> ad17a7ae5f5d05e67d0e57ed89f30e09932fffb8
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
        public ModelDrawable Model;

        
        public MeshDrawable(ModelDrawable model, Mesh assimpMesh){
            Model = model;
            Model.Materials[assimpMesh.MaterialIndex].Add(this);
            TextureCoords = assimpMesh.TextureCoordinateChannels;
            Indices = assimpMesh.GetIndices();
            Vertices = assimpMesh.Vertices.ToArray();
            Name = assimpMesh.Name;

<<<<<<< HEAD
        }
        protected override DrawNode CreateDrawNode()
        {
            return (new MeshDrawNode<TexturedMeshVertex>(this));
=======
        public MeshDrawable(Mesh mesh, Material material)
        {
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

        protected class MeshDrawNode<T> : DrawNode
            where T : unmanaged, IMeshVertex<T>
        {
            private IVertexBatch<T> vertexBatch;
            protected new MeshDrawable Source => (MeshDrawable)base.Source;


            public MeshDrawNode(MeshDrawable source) : base(source)
            {
            }




            protected override void Draw(IRenderer renderer)
            {
                vertexBatch ??= renderer.CreateLinearBatch<T>(Source.Indices.Length * 3, 3, PrimitiveTopology.Triangles);
                renderer.BindTexture(renderer.WhitePixel);
                for (int i = 0; i < Source.Indices.Length; i++)
                {
                    vertexBatch.AddAction(T.FromMesh(Source, Source.Indices[i]));
                }
            }
>>>>>>> ad17a7ae5f5d05e67d0e57ed89f30e09932fffb8
        }
    }
}
