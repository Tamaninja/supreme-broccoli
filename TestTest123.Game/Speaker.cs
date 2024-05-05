
using System.Diagnostics;
using System.Runtime.CompilerServices;
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


namespace TestTest123.Game
{
    public partial class Speaker : Container
    {
        private SpriteText spriteText;
        private int playbackDevice;
        private int recordingDevice;
        private WasapiProcedure wasapiProcedure;


        public Speaker(SpriteText text)
        {
            RelativeSizeAxes =  Axes.Both;


            BasicScrollContainer scroll = new BasicScrollContainer();
            scroll.RelativeSizeAxes = Axes.Both;
            scroll.Size = new Vector2(0.33f, 0.33f);
            

            MarkdownList markdownList = new MarkdownList();
            MarginPadding marginPadding = new MarginPadding();

            scroll.Add(markdownList);
            

            for (int i = 0; i < BassWasapi.DeviceCount; i++)
            {
                BassWasapi.GetDeviceInfo(i, out WasapiDeviceInfo deviceInfo);


                Container device = new Container();
                device.RelativeSizeAxes = Axes.X;
                device.Height = 100f;


                Box box = new Box();
                box.Colour = Colour4.AliceBlue.Opacity(0.5f);
                box.RelativeSizeAxes = Axes.Both;

                TextFlowContainer textFlowContainer = new TextFlowContainer();
                
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
            this.Add(scroll);
            spriteText = text;
           

        }
        protected override void LoadComplete()
        {
            Test test = new Test();
            wasapiProcedure = (buffer, length, user) =>
            {

                test.PutData(new WasapiDTO(buffer, length, user));
                return (length);
            };



            InitiateWasapiDevice(BassWasapi.DefaultInputDevice, procedure: wasapiProcedure, Flags: WasapiInitFlags.Exclusive);
            base.LoadComplete();
        }

        


        public static void InitiateWasapiDevice(int deviceId, WasapiProcedure procedure = null, WasapiInitFlags Flags = WasapiInitFlags.Shared)
        {
            bool initiated = BassWasapi.Init(deviceId, 48000 , Procedure: procedure, Flags: Flags);
            if (!initiated)
            {
                Logger.LogPrint("Initiated failed, requested device: " + deviceId
                    + "Error: " + Bass.LastError);

                return;
            }
            BassWasapi.GetDeviceInfo(BassWasapi.CurrentDevice, out WasapiDeviceInfo deviceInfo);


            Logger.LogPrint("deviceId: " + BassWasapi.CurrentDevice);
            Logger.LogPrint("Format: " + deviceInfo.MixFrequency);
            Logger.LogPrint("device name: " + deviceInfo.Name);
            Logger.LogPrint("device started?: " + BassWasapi.Start());


        }


        public readonly struct WasapiDTO(nint buffer, int length, nint user)
        {
            public readonly nint Buffer = buffer;
            public readonly int Length = length;
            public readonly nint User = user;
        }
    }

}
