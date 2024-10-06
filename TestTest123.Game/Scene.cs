
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
        private NotePool notePool;
        private Bindable<int> currentSongTime = new(0);

        public Scene()
        {
            RelativeSizeAxes = Axes.Both;
            Debug = new SpriteText();
            notePool = new NotePool();

            AddInternal(notePool);
            AddInternal(Debug);
        }

        public override bool PropagatePositionalInputSubTree => true;

        [BackgroundDependencyLoader]
        private void load(IRenderer renderer)
        {
            Camera = new Camera(50, 16 / 9, 1, 5000);
            AddInternal(Camera);

            MusicalChart musicalChart = new MusicalChart("C:\\Users\\Tamaninja\\Desktop\\psarc test\\Cortex_Chanson-dun-jour-dhiver_v1_p\\arr_bass_RS2.xml", Time.Current + 5000);
            currentSongTime.BindValueChanged((t) => Debug.Text = "(" + t.NewValue + "/" + musicalChart.Duration + ")");

            for (int i = 0; i < musicalChart.Notes.Count; i++)
            {
                {

                    NoteDrawable note = notePool.GenerateDrawable(musicalChart.Notes[i]);

                    AddInternal(note);


                    note.BindCamera(Camera);
                    note.OnLoadComplete += (t) => Camera.LookAt(note.Position3D);

                }

            }
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
