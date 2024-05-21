
using Assimp;
using osu.Framework.Graphics.Textures;
using osu.Framework.Logging;

namespace TestTest123.Game
{
    public partial class Material
    {
        public Texture Texture { get; }
        public ITextureStore Store { get; }
        public Color4D ColorDiffuse { get; set; }

        public Material(Assimp.Material material, ITextureStore textureStore) {
            Store = textureStore;
            ColorDiffuse = material.ColorDiffuse;
            foreach (TextureSlot texture in material.GetAllMaterialTextures())
            {
                Logger.LogPrint(texture.FilePath + " ");
                Texture = Store.Get(texture.FilePath);

            }
        }
    }
}
