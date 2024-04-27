
using System;
using System.Runtime.InteropServices;
using HidSharp.Reports.Units;
using ManagedBass;
using ManagedBass.Mix;
using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Graphics.Audio;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;


namespace TestTest123.Game
{
    public partial class Speaker : DrawableAudioWrapper
    {
        private SpriteText spriteText;
        
        public Speaker(SpriteText text) : base(new Container()) {
            spriteText = text;

            Start();
        }


        [BackgroundDependencyLoader]
        private void load(AudioManager audioManager)
        {
            Bass.Init(-1, 48000, DeviceInitFlags.Hog);
        }

        private const int DefaultFrequency = 48000; // Standard audio sample rate

        public void Start()
        {
            Bass.RecordInit(4);
            Bass.RecordGetDeviceInfo(4);
            Bass.Configure(Configuration.PlaybackBufferLength, 10);
            Bass.Configure(Configuration.UpdatePeriod, 5);

            int recordHandle = Bass.RecordStart(DefaultFrequency, 2,BassFlags.Default , myRecordingCallback);
            int sampleHandle = Bass.CreateStream(DefaultFrequency, 2, BassFlags.Default, StreamProcedureType.Push);
            Bass.ChannelPlay(sampleHandle);

        }

        private bool myRecordingCallback(int handle, IntPtr buffer, int length, IntPtr user)
        {

            Bass.StreamPutData(sampleHandle, buffer, length);

            return (true);
        }
    }       
}
