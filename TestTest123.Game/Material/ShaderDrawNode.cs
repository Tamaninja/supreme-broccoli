

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
    public class ShaderDrawNode : ThreeDimensionalDrawNode
    {


        public ShaderDrawNode(Scene scene, IShader shader) : base(scene.Node)
        {
            TextureShader = shader;
            Name = "Shader";
        }

       public override void Draw(IRenderer renderer)
       {
            TextureShader.Bind();

                base.Draw(renderer);

            TextureShader.Unbind();
        }
     }
}
