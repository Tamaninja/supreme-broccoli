
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO.Pipes;
using System.Runtime.CompilerServices;
using System.Threading;
using ManagedBass;
using ManagedBass.Wasapi;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Containers.Markdown;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Logging;
using osu.Framework.Text;
using osuTK;


namespace TestTest123.Game.Audio
{
    public partial class Speaker : Container
    {
        private SpriteText spriteText;
        private int playbackDevice;
        private int recordingDevice;


        public Speaker(SpriteText text)
        {
            RelativeSizeAxes = Axes.Both;


            var scroll = new BasicScrollContainer();
            scroll.RelativeSizeAxes = Axes.Both;
            scroll.Size = new Vector2(0.33f, 0.33f);


            var markdownList = new MarkdownList();
            var marginPadding = new MarginPadding();

            scroll.Add(markdownList);


            for (var i = 0; i < BassWasapi.DeviceCount; i++)
            {
                BassWasapi.GetDeviceInfo(i, out var deviceInfo);


                var device = new Container();
                device.RelativeSizeAxes = Axes.X;
                device.Height = 100f;


                var box = new Box();
                box.Colour = Colour4.AliceBlue.Opacity(0.5f);
                box.RelativeSizeAxes = Axes.Both;

                var textFlowContainer = new TextFlowContainer();

                textFlowContainer.AddParagraph("DeviceId: " + i + "\n " +
                    "DeviceName: " + deviceInfo.Name + "\n " +
                    "Frequency: " + deviceInfo.MixFrequency + "\n " +
                    "Channels: " + deviceInfo.MixChannels + "\n " +
                    "init: " + deviceInfo.IsInitialized);
                textFlowContainer.RelativeSizeAxes = Axes.Both;


                device.Add(textFlowContainer);
                device.Add(box);



                markdownList.Insert(i, device);
            }
            Add(scroll);
            spriteText = text;


        }
        protected override void LoadComplete()
        {

            WasapiDevice outputDevice = new WasapiDevice(BassWasapi.DefaultDevice, false);
            WasapiDevice inputDevice = new WasapiDevice(BassWasapi.DefaultInputDevice, true, Flags: WasapiInitFlags.Exclusive);


            base.LoadComplete();
        }
        
    }

}
