using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ManagedBass;
using ManagedBass.Wasapi;
using osu.Framework.Logging;
using static TestTest123.Game.Audio.Speaker;

namespace TestTest123.Game.Audio
{
    public class WasapiDevice
    {
        public WasapiDeviceInfo DeviceInfo { get; private set; }
        public static BlockingCollection<WasapiDTO> SharedBuffer = new();
        private WasapiProcedure wasapiProcedure;
        private int deviceId;


        public WasapiDevice(int deviceId, bool isInput, WasapiInitFlags Flags = WasapiInitFlags.Shared) {
            this.deviceId = deviceId;

            if (isInput)
            {
                wasapiProcedure = (buffer, length, user) =>
                {
                    SharedBuffer.Add(new WasapiDTO(buffer, length, user));
                    return length;
                };
            }

            InitiateWasapiDevice(isInput, Flags);

        }
        private void outputThread()
        {
            var outputThread = new Thread(() =>
            {
                BassWasapi.CurrentDevice = deviceId;
                BassWasapi.Start();

                while (true)
                {
                    SharedBuffer.TryTake(out var incoming);
                    BassWasapi.PutData(incoming.Buffer, incoming.Length);
                }
            });
            outputThread.Start();

        }
        private void inputThread()
        {
            var inputThread = new Thread(() =>
            {
                BassWasapi.CurrentDevice = deviceId;
                BassWasapi.Start();

                while (true)
                {

                }
            });
            inputThread.Start();
        }


        public Errors InitiateWasapiDevice(bool isInput, WasapiInitFlags Flags = WasapiInitFlags.Shared)
        {
            var initialized = BassWasapi.Init(deviceId, 48000, Procedure: wasapiProcedure, Flags: Flags, Buffer: 0.02f, Period: 0.01f);
            if (initialized)
            {
                deviceId = BassWasapi.CurrentDevice;
                DeviceInfo = BassWasapi.GetDeviceInfo(deviceId);

                if (!DeviceInfo.IsInput)
                {
                    outputThread();
                } else
                {
                    inputThread();
                }

            }

            return (Bass.LastError);
        }



        public readonly struct WasapiDTO(nint buffer, int length, nint user)
        {
            public readonly nint Buffer = buffer;
            public readonly int Length = length;
            public readonly nint User = user;
        }
    }
}
