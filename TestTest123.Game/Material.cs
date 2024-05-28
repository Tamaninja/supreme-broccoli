

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
    public partial class Material : Container, ITexturedShaderDrawable
    {

        public IShader TextureShader { get; protected set; }
        public string Texture { get; protected set; }
        public ThreeDimensionalStageDrawable Stage { get; protected set; }

        public bool IsTextured => Texture != null;

        public Material(ThreeDimensionalStageDrawable stage, Assimp.Material material)
        {
            Stage = stage;
            Colour = material.ColorDiffuse.FromAssimp();
            Name = material.Name;

            if (material.HasTextureDiffuse)
            {
                Texture = material.TextureDiffuse.FilePath;
            }
        }



        [BackgroundDependencyLoader]
        private void load(IRenderer renderer, ShaderManager shaders)
        {
            TextureShader = shaders.Load("textureless", "textureless");
            if (IsTextured)
            {
                TextureShader = shaders.Load("nino", "nino");
            }

        }

        protected override DrawNode CreateDrawNode()
        {
            return (new MaterialDrawNode(this));
        }


        protected class MaterialDrawNode : CompositeDrawableDrawNode
        {
            private IShader shader;
            private Texture texture;
            protected new Material Source => (Material)base.Source;

            public MaterialDrawNode(Material source) : base(source)
            {
                
            }

            public override void ApplyState()
            {
                base.ApplyState();
                shader = Source.TextureShader;
                texture = Source.Stage.GetTextureBypassAtlas(Source.Texture);
            }


            protected override void Draw(IRenderer renderer)
            {
                texture ??= renderer.WhitePixel;

                shader.Bind();
                texture.Bind();
                    base.Draw(renderer);
                shader.Unbind();
            }
        }
    }
}
