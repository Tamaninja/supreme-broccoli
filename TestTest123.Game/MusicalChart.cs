using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Pooling;
using osuTK;
using osuTK.Graphics;
using Rocksmith2014.XML;

namespace TestTest123.Game
{
    public partial class MusicalChart
    {
        public readonly int Duration;
        private InstrumentalArrangement arrangement;
        private List<TunableString> strings = [];
        public List<Measure> Measures;
        public List<Note> Notes { get; private set; }

        public MusicalChart(string xmlFilePath, double startTime)
        {

            arrangement = InstrumentalArrangement.Load(xmlFilePath);
            Duration = arrangement.MetaData.SongLength;

            initializeStrings();

            Notes = arrangement.Levels[0].Notes;
            
            
        }

        private void initializeStrings()
        {
            for (int i = 0; i < 4; i++)
            {
                strings.Add(new TunableString(i));
            }
        }

    }

    public class TunableString
    {
        public int Index { get; }
        public Color4 Color { get; private set; }
        public float YOffset { get; private set; }
        public sbyte Tuning { get; set; }
        public float Hz { get; set; }


        public TunableString(int index)
        {
            Index = index;
            Color = Utils.StringColors(index);
            YOffset = -index;
        }
    }
}
