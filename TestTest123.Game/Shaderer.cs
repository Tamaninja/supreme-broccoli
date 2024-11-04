

using System;
using System.Collections.Generic;
using Assimp;
using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Textures;

namespace TestTest123.Game
{
    public class Shaderer : ThreeDimensionalDrawNode
    {
        private IUniformBuffer<UniformMaterial> uniformBuffer;
        protected IShader Shader;
        public Shaderer(IShader shader, SceneNode scene) : base(scene)
        {
            Name.Value = "Shaderer";
            Shader = shader;
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
