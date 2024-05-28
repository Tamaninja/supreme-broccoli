using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Rendering.Vertices;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Logging;
using osuTK;
using osuTK.Graphics;
using TestTest123.Game.Vertices;

namespace TestTest123.Game
{
    public partial class MeshDrawable : Component, ITexturedShaderDrawable
    {
        
        public Mesh Mesh;
        public Matrix4 LocalMatrix;
        public Texture Texture;
        public IShader TextureShader { get; set; }
        public ModelDrawable Model { get; set; }


        public MeshDrawable(Mesh mesh, ModelDrawable parent){
            Model = parent;
            Colour = mesh.Material.Colour;
            Mesh = mesh;
            LocalMatrix = Matrix4.Identity;
        }


        [BackgroundDependencyLoader]
        private void load(ShaderManager shaders, IRenderer renderer, TextureStore textureStore)
        {
            TextureShader = shaders.Load("textureless", "textureless");
            if (Mesh.Material.IsTextured)
            {
                TextureShader = shaders.Load("nino", "nino");
                Texture = textureStore.Get(Mesh.Material.TextureKey);
            }
            else
            {
                Texture = renderer.WhitePixel;
            }
        }
        protected override DrawNode CreateDrawNode()
        {
            return (new MeshDrawNode(this));

        }
    }
}
