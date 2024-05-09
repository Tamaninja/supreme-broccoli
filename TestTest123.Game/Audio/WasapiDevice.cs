using System;
using HidSharp;
using ManagedBass;
using ManagedBass.Wasapi;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Logging;

namespace TestTest123.Game.Audio
{
    public class WasapiDevice
    {
        public WasapiDeviceInfo DeviceInfo { get; private set; }
        public WasapiProcedure Procedure { get; set; }
        public int Index { get; private set; }

        private WasapiDevice(int deviceId)
        {
            Index = deviceId;
            DeviceInfo = BassWasapi.GetDeviceInfo(deviceId);

        }

        public static WasapiDevice FromIndex(int index)
        {
            return new WasapiDevice(index);
        }

        public bool Initiate(int Frequency = 0, int Channels = 0, WasapiInitFlags Flags = WasapiInitFlags.Shared, float Buffer = 0f, float Period = 0f, IntPtr User = default)
        {

            return (BassWasapi.Init(Index, Frequency, Channels, Flags, Buffer, Period, Procedure, User));

        }

        public override string ToString()
        {
            return DeviceInfo.ToString();
        }

        public string GetInfo()
        {
            return
                   $"Minimum Update Period (seconds): {DeviceInfo.MinimumUpdatePeriod}\n" +
                   $"Mix Frequency: {DeviceInfo.MixFrequency}\n" +
                   $"Mix Channels: {DeviceInfo.MixChannels}\n" +
                   $"loopback: {DeviceInfo.IsLoopback}\n" +

                   $"Is Input Device: {DeviceInfo.IsInput}\n";
        }
    }
}
