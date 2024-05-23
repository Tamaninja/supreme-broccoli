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
using osu.Framework.Testing;
using osuTK;
using TestTest123.Game.Vertices;
namespace TestTest123.Game
{
    public partial class ModelDrawable : ThreeDimensionalDrawable, ITexturedShaderDrawable
    {


        public IShader TextureShader {  get; set; }
        public Type VertexType;

        public List<Material> Materials = new List<Material>();
        public bool IsTextured { get; private set; } = false;
        public readonly string FilePath;
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
                    Materials[mesh.MaterialIndex].Add(createNewMesh(mesh));

                }
            }
        }
        private MeshDrawable createNewMesh(Mesh mesh)
        {
            
            MeshDrawable drawable = new MeshDrawable(mesh, Materials[mesh.MaterialIndex]);
            return( drawable );
        }
        private void loadMaterials(Scene sceneInfo)
        {
            for (int i = 0; i < sceneInfo.Materials.Count; i++)
            {
                if (sceneInfo.Materials[i].GetAllMaterialTextures().Length > 0)
                {
                    IsTextured = true;
                }
                Material material = new Material(sceneInfo.Materials[i]);
                Materials.Add(material);
                AddInternal(material);
            }
            loadMeshes(sceneInfo);

        }


        [BackgroundDependencyLoader]
        private void load(ShaderManager shaders, TextureStore textureStore)
        {

            AssimpContext Importer = new AssimpContext();
            if (FilePath != null)
            {
                Scene sceneInfo = Importer.ImportFile(FilePath, PostProcessSteps.Triangulate | PostProcessSteps.FlipUVs);

                loadMaterials(sceneInfo);

            }


            TextureShader = shaders.Load("textureless", "textureless");
            if (IsTextured)
            {
                TextureShader = shaders.Load("nino", "nino");

            }

        }

        protected override DrawNode CreateDrawNode()
        {
            if (IsTextured)
            {
                return (new ModelDrawNode<TexturedMeshVertex>(this));

            }
            else
            {
                return (new ModelDrawNode<TexturelessMeshVertex>(this));

            }

        }

        protected class ModelDrawNode<T> : CompositeDrawableDrawNode where T : unmanaged, IMeshVertex<T>
        {
            private IVertexBatch<T> vertexBatch;

            private ModelDrawable model;
            public ModelDrawNode(ModelDrawable source) : base(source)
            {
                model = source;
            }

            

            protected override void Draw(IRenderer renderer)
            {


                vertexBatch ??= renderer.CreateLinearBatch<T>(10000, 3, PrimitiveTopology.Triangles);


                model.TextureShader.Bind();
                renderer.PushDepthInfo(DepthInfo.Default);
                renderer.PushProjectionMatrix(model.GetLocalMatrix() * model.Camera.GetViewMatrix() * model.Camera.GetProjectionMatrix());

                
                foreach (MeshDrawable mesh in model.ChildrenOfType<MeshDrawable>())
                {
                    mesh.Draw(vertexBatch);
                }

                renderer.PopDepthInfo();
                renderer.PopProjectionMatrix();
                model.TextureShader.Unbind();
            }
        }
    }
}
