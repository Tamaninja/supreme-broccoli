using System;
using Assimp;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Pooling;
using osuTK;
using Rocksmith2014.XML;

namespace TestTest123.Game
{
    public partial class NotePool : Container
    {
        private DrawablePool<NoteDrawable> drawablePool;
        private Colour4[] colorTable;

        public NotePool() {
            drawablePool = new DrawablePool<NoteDrawable>(100);
            Add(drawablePool);
            colorTable = [Colour4.Red, Colour4.Yellow, Colour4.Blue, Colour4.Orange];
        }

        public NoteDrawable GenerateDrawable(Note note)
        {

            NoteDrawable drawable = drawablePool.Get((t) => ApplyEffects(t, note));

            return (drawable);
        }


        public virtual void ApplyEffects(NoteDrawable drawable, Note note) {
            drawable.LifetimeStart = -10000 + Time.Current + note.Time;
            drawable.LifetimeEnd = 10000 + Time.Current + note.Time;

            drawable.Colour = colorTable[note.String];
            drawable.Position3D = new Vector3(note.Fret * 2, -note.String, note.Time / 100);
            drawable.Scale3D = (new Vector3(0.5f));
    }
    }
}
