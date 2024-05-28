

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
    public partial class Material : Component, ITexturedShaderDrawable
    {

        public IShader TextureShader { get; protected set; }
        public Texture Texture { get; protected set; }
        public string TextureKey {  get; protected set; }
        public ThreeDimensionalStageDrawable Stage { get; protected set; }

        public bool IsTextured => TextureKey != null;

        public Material(ThreeDimensionalStageDrawable stage, Assimp.Material material)
        {
            Stage = stage;
            Colour = material.ColorDiffuse.FromAssimp();
            Name = material.Name;

            if (material.HasTextureDiffuse)
            {
                TextureKey = material.TextureDiffuse.FilePath;
            }
        }



        [BackgroundDependencyLoader]
        private void load(ShaderManager shaders, IRenderer renderer)
        {
            TextureShader = shaders.Load("textureless", "textureless");
            if (IsTextured)
            {
                TextureShader = shaders.Load("nino", "nino");
                Texture = Stage.GetTextureBypassAtlas(TextureKey);
            } else
            {
                Texture = renderer.WhitePixel;
            }
        }


        public void Bind()
        {
            if (!TextureShader.IsBound)
            {
                TextureShader.Bind();
            }
            Texture?.Bind();
            
        }

        public void Unbind()
        {
            TextureShader?.Unbind();
        }

    }
}
