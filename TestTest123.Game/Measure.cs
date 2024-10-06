using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTest123.Game
{
    public class Measure
    {
        public float BPM { get; private set; }

        public Beat Signature { get; private set; }


    }

    public class Beat
    {

    }

    public class NoteDuration
    {
        
    }

    public enum SemiTones
    {
        C = 0,
        C_SHARP = 1,
        D_FLAT = 1,
        D = 2,
        D_SHARP = 3,
        E_FLAT = 3,
        E = 4,
        F = 5,
        F_SHARP = 6,
        G_FLAT = 6,
        G = 7,
        G_SHARP = 8,
        A_FLAT = 8,
        A = 9,
        A_SHARP = 10,
        B_FLAT = 10,
        B = 11,
    }

    public struct Octave
    {
        public const int REFERENCE_INDEX = 57;
        public int Index;
        public float A4;
        public float[] Frequency;

        public Octave(int index, float a4 = 440)
        {
            Frequency = new float[12];
            Index = index;
            A4 = a4;

            int offset = REFERENCE_INDEX - (index * 12);

            for (int i = 0; i < 12; i++)
            {
                Frequency[i] = A4 * MathF.Pow(2, offset / 12);
            }
        }
    }
    public class FrequencyChart
    {
        public readonly float A4;
        public Dictionary<int, Octave> Octaves { get; private set; } = [];


        public FrequencyChart(float a4 = 440)
        {
            A4 = a4;
        }

        public Octave GetOctave(int index)
        {
            if (!Octaves.TryGetValue(index, out Octave octave))
            {
                octave = new Octave(index, A4);
            }

            return(octave);
        }

    }
}
