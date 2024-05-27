using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Assimp;
using HidSharp.Reports;
using NuGet.Protocol;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Extensions.EnumExtensions;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Pooling;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Rendering.Vertices;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Shaders.Types;
using osu.Framework.Graphics.Textures;
using osu.Framework.Layout;
using osu.Framework.Logging;
using osu.Framework.Testing;
using osuTK;
using osuTK.Graphics;
using TestTest123.Game.Vertices;
namespace TestTest123.Game
{
    public partial class ModelDrawable : ThreeDimensionalDrawable, ITexturedShaderDrawable
    {
        public IShader TextureShader { get; private set; }

        public List<Material> Materials = new List<Material>();
        public List<MeshDrawable> Meshes = new List<MeshDrawable>();

        public bool IsTextured { get; private set; } = false;
        public readonly string FilePath;
        public ThreeDimensionalStageDrawable Stage;

        public override DrawColourInfo DrawColourInfo => new DrawColourInfo(Colour.AverageColour);
        public ModelDrawable(string filepath, ThreeDimensionalStageDrawable stage)
        {
            Stage = stage;
            FilePath = filepath;
        }

        protected override bool OnInvalidate(Invalidation invalidation, InvalidationSource source)
        {
            if (invalidation.HasFlagFast(Invalidation.Colour))
            {
                foreach (MeshDrawable mesh in Meshes)
                {

                    mesh.Colour = Colour;
                }
            }

            return base.OnInvalidate(invalidation, source);
        }

        [BackgroundDependencyLoader]
        private void load(ShaderManager shaders, TextureStore textureStore)
        {
            
            AssimpContext importer = new AssimpContext();

            if (FilePath != null)
            {
                Scene sceneInfo = importer.ImportFile(FilePath, PostProcessSteps.Triangulate | PostProcessSteps.FlipUVs);
                loadMaterials(sceneInfo);
            }

            TextureShader = shaders.Load("textureless", "textureless");

            if (IsTextured)
            {
                TextureShader = shaders.Load("nino", "nino");
            }
        }

        private void loadMeshes(Scene sceneInfo)
        {
            if (!sceneInfo.HasMeshes) { return; }


            foreach (Mesh mesh in sceneInfo.Meshes)
            {
                generateNewMesh(mesh);
            }
       }

        private void generateNewMesh(Mesh mesh)
        {
            MeshDrawable meshDrawable = new MeshDrawable(mesh, Materials[mesh.MaterialIndex]);
            meshDrawable.Colour = Colour;
            AddInternal(meshDrawable);
        }
        protected virtual void AddInternal(MeshDrawable mesh)
        {
            base.AddInternal(mesh);
            Meshes.Add(mesh);
        }

        private void loadMaterials(Scene sceneInfo)
        {

            foreach (Assimp.Material assimp in sceneInfo.Materials)
            {
                Material material = Stage.GetMaterial(GetType(), assimp);
                Materials.Add(material);

                if (assimp.GetAllMaterialTextures().Length > 0)
                {
                    IsTextured = true;
                }
            }


            loadMeshes(sceneInfo);
        }

        protected override DrawNode CreateDrawNode()
        {
            return (new ModelDrawNode(this));
        }

        protected class ModelDrawNode : CompositeDrawableDrawNode
        {
            private Matrix4 vpMatrix = Matrix4.Identity;
            private Matrix4 modelMatrix = Matrix4.Identity;
            private IUniformBuffer<MaterialUniform> colorUniform;
            private IShader shader;
            private Vector4 color;

            public ModelDrawNode(ModelDrawable source) : base(source)
            {
            }

            protected new ModelDrawable Source => (ModelDrawable)base.Source;

            
            public void BindUniform(IShader shader, IRenderer renderer)
            {

                colorUniform ??= renderer.CreateUniformBuffer<MaterialUniform>();
                colorUniform.Data = new MaterialUniform() { Color = color};
                shader.BindUniformBlock("u_Colour", colorUniform);
                
            }

            public override void ApplyState()
            {
                base.ApplyState();

                vpMatrix = Source.Stage.Camera.VPMatrix;
                shader = Source.TextureShader;
                color = DrawColourInfo.Colour.TopLeft.ToVector();
                modelMatrix = Source.GetLocalMatrix();
            }

            protected override void Draw(IRenderer renderer)
            {

                Source.TextureShader?.Bind();
                /*                BindUniform(shader, renderer);
                */


                base.Draw(renderer);


                Source.TextureShader.Unbind();
            }

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            private record struct MaterialUniform
            {
                public UniformVector4 Color;
            }
        }
    }
}
