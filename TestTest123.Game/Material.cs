
using System.Runtime.InteropServices;
using Assimp;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Shaders.Types;
using osu.Framework.Graphics.Textures;
using osu.Framework.IO.Stores;

using osu.Framework.Platform;
using osuTK;

namespace TestTest123.Game
{
    public partial class Material : Container
    {
        public Texture Texture { get; set; }
        public Assimp.Material material; //d


        public Material(Assimp.Material material)
        {
            this.material = material;
            Colour = material.ColorDiffuse.FromAssimp();
            Name = material.Name;
            Alpha = 0;
        }



        [BackgroundDependencyLoader]
        private void load(IRenderer renderer, GameHost host, osu.Framework.Game game)
        {
            IResourceStore<TextureUpload> textureLoaderStore = null!;
            textureLoaderStore = host.CreateTextureLoaderStore(new NamespacedResourceStore<byte[]>(game.Resources, @"Textures"));
            TextureStore textureStore = new TextureStore(renderer, textureLoaderStore, false, TextureFilteringMode.Linear, true);

            foreach (TextureSlot texture in material.GetAllMaterialTextures())
            {
                Texture = textureStore.Get(texture.FilePath);
            }
        }


    }
}
