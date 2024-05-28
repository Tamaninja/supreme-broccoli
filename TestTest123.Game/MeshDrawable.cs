using System.Collections.Generic;
using Assimp;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Rendering.Vertices;
using osu.Framework.Graphics.Sprites;
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
            
            Material = model.Materials[assimpMesh.MaterialIndex];
            TextureCoords = assimpMesh.TextureCoordinateChannels;
            Indices = assimpMesh.GetIndices();
            Vertices = assimpMesh.Vertices.ToArray();
            Name = assimpMesh.Name;

        }
        protected override DrawNode CreateDrawNode()
        {

            if (Material.IsTextured) {
                return (new MeshDrawNode<TexturedMeshVertex>(this));
            }
            return (new MeshDrawNode<TexturelessMeshVertex>(this));

        }
    }
}
