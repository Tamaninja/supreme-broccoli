using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Assimp;
using HidSharp.Reports;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Pooling;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Rendering.Vertices;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Textures;
using osu.Framework.Logging;
using osu.Framework.Testing;
using osuTK;
using TestTest123.Game.Vertices;
namespace TestTest123.Game
{
    public partial class ModelDrawable : ThreeDimensionalDrawable, ITexturedShaderDrawable
    {
        public IShader TextureShader { get; private set; }
        public Type VertexType;

        public List<Material> Materials = new List<Material>();
        public bool IsTextured { get; private set; } = false;
        public readonly string FilePath;
        public Matrix4 VPMatrix = Matrix4.Identity;
        public Camera Camera;
        public ModelDrawable(string filepath)
        {
            RelativeSizeAxes = Axes.Both;
            Size = Vector2.One;

            FilePath = filepath;
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
            if (sceneInfo.HasMeshes)
            {
                foreach (Mesh mesh in sceneInfo.Meshes)
                {
                    Materials[mesh.MaterialIndex].Add(createNewMesh(mesh));
                }
            }
        }

        private MeshDrawable createNewMesh(Mesh mesh)
        {
            MeshDrawable drawable = new MeshDrawable(mesh, Materials[mesh.MaterialIndex]);
            return (drawable);
        }

        private void loadMaterials(Scene sceneInfo)
        {
            for (int i = 0; i < sceneInfo.Materials.Count; i++)
            {
                Material material = new Material(sceneInfo.Materials[i]);
                AddInternal(material);

                if (sceneInfo.Materials[i].GetAllMaterialTextures().Length > 0)
                {
                    IsTextured = true;
                }
            }

            loadMeshes(sceneInfo);
        }

        protected void AddInternal(Material material)
        {
            base.AddInternal(material);
            Materials.Add(material);
        }

        protected override DrawNode CreateDrawNode()
        {
            return (new ModelDrawNode(this));
        }

        protected class ModelDrawNode : CompositeDrawableDrawNode
        {
            private Matrix4 vpMatrix;
            public ModelDrawNode(ModelDrawable source) : base(source)
            {

            }

            protected new ModelDrawable Source => (ModelDrawable)base.Source;

            public override void ApplyState()
            {
                vpMatrix = Source.Camera.VPMatrix;
                base.ApplyState();
            }

            protected override void Draw(IRenderer renderer)
            {

                Source.TextureShader!.Bind();
                renderer.PushDepthInfo(DepthInfo.Default);
                renderer.PushProjectionMatrix(Source.GetLocalMatrix() * vpMatrix);

                base.Draw(renderer);

                renderer.PopDepthInfo();
                renderer.PopProjectionMatrix();
                Source.TextureShader!.Unbind();
            }
        }
    }
}
