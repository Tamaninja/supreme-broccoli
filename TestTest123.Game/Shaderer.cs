

using System;
using Assimp;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Textures;
using TestTest123.Game.Material;

namespace TestTest123.Game
{
    public class Shaderer : ThreeDimensionalDrawNode
    {
        private IUniformBuffer<UniformMaterial> uniformBuffer;
        protected IShader Shader;
        public Shaderer(SceneNode scene, IShader shader) : base(scene)
        {
            Shader = shader;
            scene.Shaderer = this;
            Name = "Shaderer";
        }


        public void BindUniform(UniformMaterial uniformMaterial)
        {
            uniformBuffer.Data = uniformMaterial;
            Shader.BindUniformBlock("u_Colour", uniformBuffer);
        }
        public override void Draw(IRenderer renderer)
        {

            uniformBuffer ??= renderer.CreateUniformBuffer<UniformMaterial>();

            Shader.Bind();

            base.Draw(renderer);

            Shader.Unbind();
        }
    }
}
