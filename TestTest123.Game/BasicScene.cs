using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Textures;
using osuTK;

namespace TestTest123.Game
{
    public partial class BasicScene : Scene
    {
        public BasicScene() {
            AddInternal(new CameraDrawable(this, 50, 16 / 9, 1f, 5000));
        }



        [BackgroundDependencyLoader]
        private void load(ShaderManager shaders, IRenderer renderer, LargeTextureStore textureStore)
        {

            var musicalChart = new MusicalChart("C:\\Users\\lielk\\OneDrive\\Desktop\\psarc tests\\Telula_3-3-3_v1_p\\arr_bass_RS2.xml");

            var mcqueen = new NoteDrawable(Node);
/*            var trashcan = new NoteDrawable(Node);

            NodeInstance newInstance = new NodeInstance(trashcan);*/
            for (int i = 0; i < musicalChart.Notes.Length; i++)
            {
                {
                    var instance = new NodeInstance(mcqueen);
                    instance.Position.Value = new Vector3(musicalChart.Notes[i].Fret * 4, -musicalChart.Notes[i].String * 4, musicalChart.Notes[i].Time / 20);
                    instance.Scale.Value = new Vector3(5f, 5f, (musicalChart.Notes[i].Sustain + 100f) / 20);
                    instance.Colour = NoteDrawable.ColorTable[musicalChart.Notes[i].String];



                }

            }

        }
    }
}
