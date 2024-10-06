

using System;
using Assimp;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Textures;
using TestTest123.Game;

namespace TestTest123.Game.Material
{
    public partial class MaterialDrawable : Container, ITexturedShaderDrawable
    {


        private string textureKey;
        public bool IsTextured => textureKey != null;

        public IShader TextureShader { get; private set; }
        public Texture Texture { get; private set; }
        private Type type { get; set; }


        public MaterialDrawable(Assimp.Material material)
        {
            Name = material.Name;
            Colour = material.ColorDiffuse.FromAssimp();
            Alpha = material.ColorDiffuse.A;
            RelativeSizeAxes = Axes.Both;


            if (material.HasTextureDiffuse)
            {
                textureKey = material.TextureDiffuse.FilePath;
            }

        }



        [BackgroundDependencyLoader]
        private void load(ShaderManager shaders, IRenderer renderer, LargeTextureStore textureStore)
        {

            Texture = renderer.WhitePixel;
            TextureShader = shaders.Load("textureless", "textureless");

            if (IsTextured)
            {
                Texture = textureStore.Get(textureKey);
            }

        }


        protected override DrawNode CreateDrawNode()
        {
            return new MaterialDrawNode(this);
        }

        protected class MaterialDrawNode : CompositeDrawableDrawNode
        {
            protected new MaterialDrawable Source => (MaterialDrawable)base.Source;
            private Texture texture;
            private IShader shader;
            public MaterialDrawNode(MaterialDrawable source) : base(source)
            {

                texture = source.Texture;
            }

            public override void ApplyState()
            {
                base.ApplyState();
                texture = Source.Texture;
                shader = Source.TextureShader;
            }

            protected override void Draw(IRenderer renderer)
            {

                texture?.Bind();
                shader.Bind();

                base.Draw(renderer);

                shader.Unbind();

            }
        }



    }
}
