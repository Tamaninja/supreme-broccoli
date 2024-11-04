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
        public Note[] Notes { get; private set; }

        public MusicalChart(string xmlFilePath)
        {

            arrangement = InstrumentalArrangement.Load(xmlFilePath);
            Duration = arrangement.MetaData.SongLength;


            Notes = arrangement.Levels[0].Notes.ToArray();
            
            
        }


    }
}
