

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

        private IUniformBuffer<MaterialData> data;


        public MaterialDrawable(Material material)
        {
            Name = material.Name;
            Colour = new Colour4(1f, 0f, 1, 1f);
            RelativeSizeAxes = Axes.Both;

            if (material.HasTextureDiffuse)
            {
                textureKey = material.TextureDiffuse.FilePath;

            }
        }

        [BackgroundDependencyLoader]
        private void load(ShaderManager shaders, IRenderer renderer, LargeTextureStore textureStore)
        {
            data = renderer.CreateUniformBuffer<MaterialData>();
            data.Data = new MaterialData { Color = Colour.TopLeft.ToVector() };


            TextureShader = shaders.Load("nino", "nino");
            if (IsTextured)
            {
                TextureShader = shaders.Load("nino", "nino");
                Texture = textureStore.Get(textureKey);
            } else
            {
                shaders.Load("textureless", "textureless");

            }
            TextureShader.BindUniformBlock("u_Colour", data);

        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private record struct MaterialData
        {
            public UniformVector4 Color;
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


                texture ??= renderer.WhitePixel;


                texture.Bind();
                shader.Bind();

                    base.Draw(renderer);

                shader.Unbind();

            }
        }



    }
}
