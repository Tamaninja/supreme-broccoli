using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Assimp;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Textures;
using SixLabors.ImageSharp.Metadata;
using TestTest123.Game.Vertices;

namespace TestTest123.Game
{
    public record class Model
    {
        public List<Material> Materials;
        public List<Mesh> Meshes;
        public readonly string Filepath;
        public Model(string filepath) {

            Filepath = filepath;
            AssimpContext importer = new AssimpContext();
            Scene sceneInfo = importer.ImportFile(filepath, PostProcessSteps.Triangulate | PostProcessSteps.FlipUVs);
            Materials = sceneInfo.Materials;
            Meshes = createMesh(sceneInfo.Meshes);
        }

        public static Model BOX_3D()
        {
            return (new Model(@"C:\Users\lielk\Documents\GitHub\supreme-broccoli\TestTest123.Resources\Models\Trashcan_Small1.fbx"));
        }
        private List<Mesh> createMesh(List<Assimp.Mesh> assimpMeshes)
        {
            List<Mesh> meshes = [];
            foreach (Assimp.Mesh mesh in assimpMeshes)
            {
                meshes.Add(new Mesh(this, mesh));
            }
            return (meshes);
        }
    }
}
