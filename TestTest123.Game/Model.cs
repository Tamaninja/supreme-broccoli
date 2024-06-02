using System.Collections.Generic;
using Assimp;

namespace TestTest123.Game
{
    public record class Model
    {
        public List<Assimp.Material> Materials;
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
            return (new Model(@"D:\Tamaninja\Documents\TestTest123\TestTest123.Resources\Models\Trashcan_Small1.fbx"));
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
