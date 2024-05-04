
using System;
using System.Threading;
using System.Threading.Tasks;
using ManagedBass;
using ManagedBass.Wasapi;
using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Bindables;
using osu.Framework.Graphics.Audio;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Logging;
using osu.Framework.Threading;


namespace TestTest123.Game
{
    public partial class Speaker : DrawableAudioWrapper
    {
        private SpriteText spriteText;
        private int playbackDevice;
        private int recordingDevice;
        private     WasapiProcedure wasapiProcedure;


        public Speaker(SpriteText text) : base(new Container())
        {
            spriteText = text;
        }


        [BackgroundDependencyLoader]
        private void load(AudioManager audioManager)
        {
            AudioInputThread audioInputThread = new AudioInputThread();

            wasapiProcedure = (buffer, length, user) =>
            {
                audioInputThread.Playback(buffer, length, user);

                return (length);
            };


            /*            for (int i = 0; i < BassWasapi.DeviceCount; i++)
                        {
                            Logger.LogPrint(i + "\t" + BassWasapi.GetDeviceInfo(i).ToString());
                            Thread.Sleep(150);

                        }*/


            Logger.LogPrint("device init: " + BassWasapi.Init(-2, 48000, Procedure: wasapiProcedure));
            Logger.LogPrint("Format: " + BassWasapi.Info.Frequency);

            Logger.LogPrint("device start: " + BassWasapi.Start());
            Logger.LogPrint("device: " + BassWasapi.CurrentDevice);

        }
    }

}
