
using Assimp;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Textures;
using osu.Framework.Logging;

namespace TestTest123.Game
{
    public partial class Material : Container
    {
        public Texture Texture { get; set; }
        public Assimp.Material material; //d

        public Material(Assimp.Material material) {
            this.material = material;
            Colour = material.ColorDiffuse.FromAssimp();
            Name = material.Name;
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textureStore)
        {
            foreach (TextureSlot texture in material.GetAllMaterialTextures())
            {
                Texture = textureStore.Get(texture.FilePath);
            }
        }
    }
}
