
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO.Pipes;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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
        private WasapiProcedure procOutput;



        public Speaker(SpriteText text)
        {
            RelativeSizeAxes = Axes.Both;
            Size = new Vector2(0.33f, 0.33f);

            BasicDropdown<WasapiDevice> outputDropdown = new BasicDropdown<WasapiDevice>();
            BasicDropdown<WasapiDevice> inputDropdown = new BasicDropdown<WasapiDevice>();

            outputDropdown.Name = "Output devices";
            inputDropdown.Name = "Input devices";


            inputDropdown.RelativeSizeAxes = Axes.X;
            outputDropdown.RelativeSizeAxes = Axes.X;
            inputDropdown.Width = 1f;
            inputDropdown.Width = 1f;


            inputDropdown.Anchor = Anchor.TopRight;

            for (int i = 0; i < BassWasapi.DeviceCount; i++)
            {
                
                WasapiDevice device = WasapiDevice.FromIndex(i);
                if (device.DeviceInfo.IsLoopback
                    || device.DeviceInfo.IsUnplugged
                    || device.DeviceInfo.IsDisabled
                    || device.DeviceInfo.MixFrequency == 0
                    ) continue;

                if(device.DeviceInfo.IsInput)
                {
                    inputDropdown.AddDropdownItem(device);
                    if (device.DeviceInfo.IsDefault)
                    {
                        inputDropdown.Current.Default = device;
                    }

                }
                else
                {
                    outputDropdown.AddDropdownItem(device);
                    if (device.DeviceInfo.IsDefault)
                    {
                        outputDropdown.Current.Default = device;
                    }

                }
            }
            inputDropdown.Current.SetDefault();
            outputDropdown.Current.SetDefault();

            Add(inputDropdown);
            Add(outputDropdown);

            
            spriteText = text;

            outputDropdown.Current.BindValueChanged((s) => {
                
                spriteText.Text = s.NewValue.GetInfo();
            });
            inputDropdown.Current.BindValueChanged((s) => {

                spriteText.Text = s.NewValue.GetInfo();
            });


        }
        protected override void LoadComplete()
        {
            Thread thread = new Thread(() =>
            {

                WasapiProcedure wasapiProcedure = (nint buffer, int length, nint user) =>
                {
                    BassWasapi.CurrentDevice = 58;
                    BassWasapi.PutData(buffer, length);

                    return (length);
                };

                WasapiDevice outputDevice = WasapiDevice.FromIndex(58);
                outputDevice.Initiate();
                BassWasapi.Start();

                WasapiDevice inputDevice = WasapiDevice.FromIndex(91);
                inputDevice.Procedure = wasapiProcedure;
                inputDevice.Initiate();
                BassWasapi.Start();

            });
            thread.Start();

            base.LoadComplete();
        }

       
    }

}
