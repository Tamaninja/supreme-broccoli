using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assimp;
using osu.Framework.Graphics.Textures;

namespace TestTest123.Game
{
    public class Model : IDisposable
    {
        public List<Material> Materials;
        public List<Mesh> Meshes;

        public Model(Scene sceneInfo) {
            Materials = loadMaterials(sceneInfo.Materials);
            Meshes = createMesh(sceneInfo.Meshes);
        }

        private List<Mesh> createMesh(List<Assimp.Mesh> assimpMeshes)
        {
            List<Mesh> meshes = [];
            foreach (Assimp.Mesh mesh in assimpMeshes)
            {
                meshes.Add(new Mesh(mesh, Materials[mesh.MaterialIndex]));
            }
            return (meshes);
        }

        private List<Material> loadMaterials(List<Assimp.Material> assimpMaterials)
        {
            List<Material> materials = [];
            foreach (Assimp.Material material in assimpMaterials)
            {
                materials.Add(new Material(material));
            }
            return(materials);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
