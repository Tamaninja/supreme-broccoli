
using System;
using System.Collections.Generic;
using Assimp;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Logging;
using osu.Framework.Testing;
using osuTK;
using osuTK.Graphics;
using osuTK.Graphics.OpenGL;
using Rocksmith2014.XML;
using Veldrid;
using Vortice;


namespace TestTest123.Game
{

    // this acts as the root for the world matrix
    public partial class Scene : Container, ITexturedShaderDrawable
    {
        public IShader TextureShader { get; set; }
        public SceneNode Node { get; private set; }

        public SpriteText Debug;
        private Bindable<int> currentSongTime = new(0);

        public List<CameraDrawable> Cameras = [];
        private CameraDrawable camera;

        public Scene()
        {
            camera = new CameraDrawable(this, 50, 16 / 9, 1f, 5000);
/*            camera.Size = new Vector2(0.5f);
*/

            Cameras.Add(camera);
            AddInternal(camera);
            RelativeSizeAxes = Axes.Both;
        }

        [BackgroundDependencyLoader]
        private void load(ShaderManager shaders, IRenderer renderer, LargeTextureStore textureStore)
        {

            TextureShader = shaders.Load("nino", "nino");
            Node = new SceneNode(this, renderer, textureStore, shaders);

            MusicalChart musicalChart = new MusicalChart("C:\\Users\\lielk\\OneDrive\\Desktop\\psarc tests\\Telula_3-3-3_v1_p\\arr_bass_RS2.xml", Time.Current + 5000);
            currentSongTime.BindValueChanged((t) => Debug.Text = "(" + t.NewValue + "/" + musicalChart.Duration + ")");

            PlaneDrawable drawable = new PlaneDrawable(Node);


            for (int i = 0; i < 55; i++)
            {
                {

                    NoteDrawable note = new NoteDrawable(musicalChart.Notes[i], Node);
                }

            }

        }

        protected override DrawNode CreateDrawNode()
        {
            return new SceneDrawNode(this);
        }

        protected class SceneDrawNode : CompositeDrawableDrawNode
        {
            protected new Scene Source => (Scene)base.Source;


            public SceneDrawNode(Scene source) : base(source)
            {


            }

            protected override void Draw(IRenderer renderer)
            {
                foreach (Shaderer shaderer in Source.Node.Shaderers.Values)
                {
                    Source.Node.CurrentShaderer = shaderer;
                    base.Draw(renderer);
                }
            }
        }
    }




    public class SceneNode : Node
    {
        public Shaderer CurrentShaderer { get; set; }
        public Dictionary<IShader, Shaderer> Shaderers = [];
        public IRenderer Renderer { get; }
        public TextureStore TextureStore { get; }
        public ShaderManager ShaderManager { get; }

        public SceneNode(Scene scene, IRenderer renderer, LargeTextureStore textureStore, ShaderManager shaderManager) : base(scene)
        {
            scene.Add(Visualization);

            Name.Value = "Scene";
            Renderer = renderer;
            TextureStore = textureStore;
            ShaderManager = shaderManager;
            Scene = this;
        }


        public Shaderer AssignShaderer(MeshDrawNode node)
        {
            IShader shader = ShaderManager.Load("nino", "nino");
            if (node.Mesh.TextureCoords.Length == 0) {
                 shader = ShaderManager.Load("textureless", "textureless");
            }

            if (!Shaderers.TryGetValue(shader, out Shaderer shaderer))
            {
                Shaderers[shader] = shaderer = new Shaderer(shader, this);
            }
            shaderer.AddSubNode(node);
            return shaderer;
        }


    }
}
