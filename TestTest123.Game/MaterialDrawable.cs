

using System;
using System.Runtime.InteropServices;
using Assimp;
using osu.Framework;
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
using osuTK.Graphics.OpenGL;
using TestTest123.Game.Vertices;
using Vortice;

namespace TestTest123.Game
{
    public partial class MaterialDrawable : Container, ITexturedShaderDrawable
    {

        private string textureKey;
        public bool IsTextured => textureKey != null;

        public IShader TextureShader {  get; private set; }
        public Texture Texture { get; private set; }



        public MaterialDrawable(Material material)
        {
            Name = material.Name;
            Colour = material.ColorDiffuse.FromAssimp();
            RelativeSizeAxes = Axes.Both;

            if (material.HasTextureDiffuse)
            {
                textureKey = material.TextureDiffuse.FilePath;

            }
        }

        [BackgroundDependencyLoader]
        private void load(ShaderManager shaders, IRenderer renderer, LargeTextureStore textureStore)
        {


            TextureShader = shaders.Load("nino", "nino");
            if (IsTextured)
            {
                TextureShader = shaders.Load("nino", "nino");
                Texture = textureStore.Get(textureKey);
            } else
            {
                shaders.Load("textureless", "textureless");

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
/*                texture = Source.Texture;
*/                shader = Source.TextureShader;
            }

            protected override void Draw(IRenderer renderer)
            {


                texture ??= renderer.WhitePixel;


                texture.Bind();
                shader.Bind();

                    base.Draw(renderer);

                shader.Unbind();

            }
        }



    }
}
