
using System;
using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Logging;
using osuTK;
using osuTK.Graphics;
using Rocksmith2014.XML;


namespace TestTest123.Game
{
    public partial class Scene : Container
    {
        public Camera Camera;
        public SpriteText Debug;
        private Bindable<int> currentSongTime = new(0);

        public Scene()
        {
            RelativeSizeAxes = Axes.Both;
            Debug = new SpriteText();

            AddInternal(Debug);
        }

        public override bool PropagatePositionalInputSubTree => true;

        [BackgroundDependencyLoader]
        private void load(IRenderer renderer)
        {
            Camera = new Camera(50, 16 / 9, 1, 5000);
            AddInternal(Camera);

            NoteDrawable note = new NoteDrawable();
            AddInternal(note);


            note.BindCamera(Camera);

            /*            MusicalChart musicalChart = new MusicalChart("C:\\Users\\lielk\\OneDrive\\Desktop\\psarc tests\\Telula_3-3-3_v1_p\\arr_bass_RS2.xml", Time.Current + 5000);
                        currentSongTime.BindValueChanged((t) => Debug.Text = "(" + t.NewValue + "/" + musicalChart.Duration + ")");

                        for (int i = 0; i < musicalChart.Notes.Count; i++)
                        {
                            {

                                NoteDrawable note = notePool.GenerateDrawable(musicalChart.Notes[i]);

                                AddInternal(note);


                                note.BindCamera(Camera);

                            }

                        }*/
        }

        protected override void Update()
        {
            base.Update();

            currentSongTime.Value = (int)Clock.CurrentTime;
/*            Camera.Position3D = new Vector3(Camera.Position3D.X, Camera.Position3D.Y, (float)(Clock.CurrentTime / 100));
*/
        }
    }
}
