
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

        private Container visualization;


        public Scene()
        {
            camera = new CameraDrawable(this, 50, 16 / 9, 1f, 5000);

            Cameras.Add(camera);
            AddInternal(camera);
            AddInternal(visualization = []);
            RelativeSizeAxes = Axes.Both;
        }

        [BackgroundDependencyLoader]
        private void load(ShaderManager shaders, IRenderer renderer, LargeTextureStore textureStore)
        {

            TextureShader = shaders.Load("nino", "nino");
            Node = new SceneNode(this, renderer, textureStore)
            {
                Name = "Scene"
            };


            /*            for (int i = 0; i < 23; i++)
                        {
                            PlaneDrawable test = new PlaneDrawable(Node);

                            AddNode(test);

                        }*/

            MusicalChart musicalChart = new MusicalChart("C:\\Users\\lielk\\OneDrive\\Desktop\\psarc tests\\Telula_3-3-3_v1_p\\arr_bass_RS2.xml", Time.Current + 5000);
            currentSongTime.BindValueChanged((t) => Debug.Text = "(" + t.NewValue + "/" + musicalChart.Duration + ")");


            for (int i = 0; i < 55; i++)
            {
                {

                    NoteDrawable note = new NoteDrawable(musicalChart.Notes[i], Node);
                    /*                    note.LifetimeStart = Time.Current + musicalChart.Notes[i].Time - NoteDrawable.PRELOAD_MS;
                                        note.LifetimeEnd = Time.Current + musicalChart.Notes[i].Time + NoteDrawable.KEEPALIVE_MS;*/
                    AddNode(note);

                }

            }

        }
        public void AddNode(ThreeDimensionalDrawNode node)
        {

            if (!Node.Shaderers.TryGetValue(TextureShader, out Shaderer shaderer))
            {
                Node.Shaderers[TextureShader] = shaderer = new Shaderer(Node, TextureShader);
                Node.AddSubNode(shaderer);
            }
            shaderer.AddSubNode(node);

            Invalidate(Invalidation.DrawNode);

            Remove(visualization, false);
            AddInternal(visualization = VisualisedNode(Node));
        }

        public Container VisualisedNode(ThreeDimensionalDrawNode node)
        {
            Container drawable = new Container
            {
                Name = node.Name,
            };
            foreach (ThreeDimensionalDrawNode subNode in node.Children)
            {
                drawable.Add(VisualisedNode(subNode));
            }
            return (drawable);
        }


    }
    public class SceneNode : ThreeDimensionalDrawNode
    {
        public Dictionary<IShader, Shaderer> Shaderers = [];

        public Shaderer Shaderer { get; set; }

        public SceneNode(Scene scene, IRenderer renderer, LargeTextureStore textureStore) : base(scene, renderer, textureStore)
        {
            Scene = this;
        }
    }
}
