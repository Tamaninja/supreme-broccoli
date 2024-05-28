
<<<<<<< HEAD

=======
using System.Runtime.InteropServices;
>>>>>>> ad17a7ae5f5d05e67d0e57ed89f30e09932fffb8
using Assimp;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Shaders.Types;
<<<<<<< HEAD
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.IO.Stores;
using osu.Framework.Logging;
=======
using osu.Framework.Graphics.Textures;
using osu.Framework.IO.Stores;

>>>>>>> ad17a7ae5f5d05e67d0e57ed89f30e09932fffb8
using osu.Framework.Platform;
using osuTK;

namespace TestTest123.Game
{
    public partial class Material : Container, ITexturedShaderDrawable
    {

<<<<<<< HEAD
        public IShader TextureShader { get; protected set; }
        public string Texture { get; protected set; }
        public ThreeDimensionalStageDrawable Stage { get; protected set; }

        public bool IsTextured => Texture != null;

        public Material(ThreeDimensionalStageDrawable stage, Assimp.Material material)
=======

        public Material(Assimp.Material material)
>>>>>>> ad17a7ae5f5d05e67d0e57ed89f30e09932fffb8
        {
            Stage = stage;
            Colour = material.ColorDiffuse.FromAssimp();
            Name = material.Name;
<<<<<<< HEAD

            if (material.HasTextureDiffuse)
            {
                Texture = material.TextureDiffuse.FilePath;
            }
        }

=======
            Alpha = 0;
        }



        [BackgroundDependencyLoader]
        private void load(IRenderer renderer, GameHost host, osu.Framework.Game game)
        {
            IResourceStore<TextureUpload> textureLoaderStore = null!;
            textureLoaderStore = host.CreateTextureLoaderStore(new NamespacedResourceStore<byte[]>(game.Resources, @"Textures"));
            TextureStore textureStore = new TextureStore(renderer, textureLoaderStore, false, TextureFilteringMode.Linear, true);
>>>>>>> ad17a7ae5f5d05e67d0e57ed89f30e09932fffb8


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
