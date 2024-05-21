using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Assimp;
using HidSharp.Reports;
using osu.Framework.Allocation;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Pooling;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Rendering.Vertices;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Textures;
using osu.Framework.Logging;
using osuTK;
namespace TestTest123.Game
{
    public partial class ModelDrawable : ThreeDimensionalDrawable, ITexturedShaderDrawable
    {


        public IShader TextureShader {  get; set; }

        public List<Material> Materials = new List<Material>();
        public bool IsTextured { get; private set; } = false;
        public readonly string FilePath;
        private ITextureStore textures { get; set; }
        public Camera Camera;


        public ModelDrawable(string filepath, Camera camera)
        {
            Camera = camera;
            RelativeSizeAxes = Axes.Both;
            Size = Vector2.One;

            FilePath = filepath;
        }


        private void loadMeshes(Scene sceneInfo)
        {
            if (sceneInfo.HasMeshes)
            {
                foreach (Mesh mesh in sceneInfo.Meshes)
                {
                    AddInternal(createNewMesh(mesh));
                }
            }
        }
        private MeshDrawable createNewMesh(Mesh mesh)
        {
            MeshDrawable drawable = new MeshDrawable(mesh, Materials[mesh.MaterialIndex]);
            return( drawable );
        }
        private void loadMaterials(List<Assimp.Material> materials)
        {
            for (int i = 0; i < materials.Count; i++)
            {
                if (materials[i].GetAllMaterialTextures().Length > 0)
                {
                    IsTextured = true;
                }
                Materials.Add(new Material(materials[i], textures));
            }
        }


        [BackgroundDependencyLoader]
        private void load(ShaderManager shaders, TextureStore textureStore)
        {

            textures = textureStore;

            AssimpContext Importer = new AssimpContext();
            if (FilePath != null)
            {
                Scene sceneInfo = Importer.ImportFile(FilePath, PostProcessSteps.Triangulate | PostProcessSteps.FlipUVs);
                if (sceneInfo.HasMaterials)
                {
                    loadMaterials(sceneInfo.Materials);
                }

                loadMeshes(sceneInfo);
            }


            TextureShader = shaders.Load("textureless", "textureless");

/*            if (SceneInfo.HasTextures)
            {
                IsTextured = true;
                foreach (EmbeddedTexture texture in SceneInfo.Textures)
                {
                    //load
                }
            }*/
            if (IsTextured)
            {
                TextureShader = shaders.Load("nino", "nino");
            }

        }

        protected override DrawNode CreateDrawNode() => new ModelDrawNode(this);


        protected class ModelDrawNode : CompositeDrawableDrawNode
        {
            private IVertexBatch<TexturelessVertex3D> vertexBatch;

            private ModelDrawable model;
            public ModelDrawNode(ModelDrawable source) : base(source)
            {
                model = source;
                
            }

            

            protected override void Draw(IRenderer renderer)
            {

                vertexBatch ??= renderer.CreateLinearBatch<TexturelessVertex3D>(10000, 3, PrimitiveTopology.Triangles);


                model.TextureShader.Bind();
                renderer.PushDepthInfo(DepthInfo.Default);
                renderer.PushProjectionMatrix(model.GetLocalMatrix() * model.Camera.GetViewMatrix() * model.Camera.GetProjectionMatrix());

                foreach (MeshDrawable mesh in model.InternalChildren)
                {
                    mesh.Draw(vertexBatch.AddAction);
                }

                renderer.PopDepthInfo();
                renderer.PopProjectionMatrix();
                model.TextureShader.Unbind();
            }
        }
    }
}
