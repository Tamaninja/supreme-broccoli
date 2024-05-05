using System.Diagnostics;
using System.Threading;
using ManagedBass.Wasapi;
using osu.Framework.Logging;
using static TestTest123.Game.Speaker;

namespace TestTest123.Game
{
    public class Test
    {
        public Test() {
            ThreadPool.SetMaxThreads(5, 2);
            ThreadPool.QueueUserWorkItem((s) =>
            {
                InitiateWasapiDevice(BassWasapi.DefaultDevice);

            });
        }

        public void PutData(WasapiDTO wasapiDTO)
        {

            ThreadPool.QueueUserWorkItem((s) => {
                BassWasapi.PutData(s.Buffer, s.Length);
            },wasapiDTO,true);

        }
    }
}
