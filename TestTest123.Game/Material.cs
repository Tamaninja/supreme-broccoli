
using Assimp;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Textures;
using osu.Framework.Logging;
using osuTK.Graphics;

namespace TestTest123.Game
{
    public partial class Material : Container
    {
        public Texture Texture { get; set; }
        public Assimp.Material material; //d        

        public Material(Assimp.Material material) {
            this.material = material;
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textureStore)
        {
            Colour = material.ColorDiffuse.FromAssimp();
            foreach (TextureSlot texture in material.GetAllMaterialTextures())
            {
                Logger.LogPrint(texture.FilePath + " ");
                Texture = textureStore.Get(texture.FilePath);

            }
        }
    }
}
