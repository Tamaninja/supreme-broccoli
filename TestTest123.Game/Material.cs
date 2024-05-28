

using Assimp;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Shaders.Types;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.IO.Stores;
using osu.Framework.Logging;
using osu.Framework.Platform;
using osuTK;

namespace TestTest123.Game
{
    public partial class Material : Component
    {

        public string TextureKey {  get; protected set; }
        public string ShaderKey { get; protected set; }
        public bool IsTextured => TextureKey != null;

        public Material(Assimp.Material material)
        {
            Colour = material.ColorDiffuse.FromAssimp();
            Name = material.Name;

            if (material.HasTextureDiffuse)
            {
                TextureKey = material.TextureDiffuse.FilePath;
            }
            ShaderKey = "textureless";
            if (IsTextured)
            {
                ShaderKey = "nino";
            }
        }

    }
}
