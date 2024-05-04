
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using ManagedBass.Wasapi;
using Markdig.Helpers;
using osu.Framework.Bindables;
using osu.Framework.Extensions;
using osu.Framework.Logging;
using osu.Framework.Threading;
using osu.Framework.Timing;

namespace TestTest123.Game
{
    public class AudioInputThread
    {
        private BlockingCollection<Task> queue = new BlockingCollection<Task>();

        public AudioInputThread(){
            ThreadPool.SetMaxThreads(5,2);
            ThreadPool.QueueUserWorkItem((s) => Init());
/*            Thread thread = new Thread(() =>
            {
            Init();

            while (BassWasapi.IsStarted)
            {

                    queue.Take().Start();

                }
            });
            thread.Start();*/
        }
       public void PutData(nint buffer, int length, nint user)
        {
            BassWasapi.PutData(buffer, length);

        }
        public void Playback(nint buffer, int length, nint user)
        {
/*            queue.Add(new Task(() => BassWasapi.PutData(buffer, length)));
*/
            ThreadPool.QueueUserWorkItem((s) => PutData(buffer, length, user), true);
           
        }

        public void Init()
        {
            
            Logger.LogPrint("device init: " + BassWasapi.Init(-1));
            Logger.LogPrint("Format: " + BassWasapi.Info.Frequency);
            Logger.LogPrint("device start: " + BassWasapi.Start());
            Logger.LogPrint("device: " + BassWasapi.CurrentDevice.ToString());

        }
    }
}
