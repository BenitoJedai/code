using System.Diagnostics;
using System.Threading;
using MP3PitchExample.ActionScript;
using ScriptCoreLib.ActionScript.BCLImplementation.System;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.system;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.Extensions;

namespace TestWorkerMP3PitchExample
{
    public sealed class ApplicationSprite : Sprite
    {
        // are we to share all static string fields in AIR as we do in chrome?
        public static string SharedField;

        public ApplicationSprite()
        {
            // X:\jsc.svn\examples\actionscript\FlashMP3PitchExperiment\FlashMP3PitchExperiment\Library\MP3Pitch.cs
            // X:\jsc.svn\examples\actionscript\Test\TestThreadStartInternalWorkerInvoke\TestThreadStartInternalWorkerInvoke\ApplicationSprite.cs

            // X:\jsc.svn\examples\actionscript\Test\TestThreadStart\TestThreadStart\ApplicationSprite.cs

            // jsc should return before getting here from the worker
            if (!Worker.current.isPrimordial)
                return;

            var t = new TextField
            {
                multiline = true,
                text = new
                {
                    __Thread.InternalPrimordialSprite,
                    this.loaderInfo.bytes.length,
                }.ToString(),
                autoSize = TextFieldAutoSize.LEFT
            };

            t.AttachTo(this);

            t.click += delegate
            {
                var sw = Stopwatch.StartNew();

                t.text = "enter click";



                __Thread tt = new Thread(
                    new ParameterizedThreadStart(
                        data =>
                        {
                            // can we render audio on the background thread now?
                            // what else can AIR do on a background thread?
                            // physics?
                            // LAN calc?

                            // how can we report to the UI thread?

                            var nn = Stopwatch.StartNew();


                            //int i = 0;

                            //// keep core2 buzy for a while to be noticed on the task manager
                            //while (nn.ElapsedMilliseconds < 10000)
                            //{
                            //    SharedField = new
                            //    {
                            //        data,
                            //        i,
                            //        nn.ElapsedMilliseconds
                            //        //, Thread.CurrentThread.ManagedThreadId 
                            //    }.ToString();

                            //    i++;
                            //}

                            // http://stackoverflow.com/questions/16483863/flash-workers-sample-application-not-working
                            // http://probertson.com/articles/2012/11/07/as3-concurrency-workers-use-cases-best-practices-links/

                            // i cant hear it
                            // http://stackoverflow.com/questions/11902863/can-actionscript-workers-be-used-to-play-generate-sounds-in-a-separate-thread
                            // http://flexmonkey.blogspot.com/2012/09/multi-threaded-sound-synthesis-in-flex.html

                            var p = new MP3Pitch("http://visit.abstractatech.com/assets/com.abstractatech.web.design1/AbstractatechPostProductionVersion7.mp3")
                            {
                                //_rate = p._rate
                            };

                            // i wonder, can we switch to UI thread via await and then back?



                            var xfromWorker = (MessageChannel)Worker.current.getSharedProperty("fromWorker");
                            // or are we to capture all fields modified within worker and only update those?
                            xfromWorker.send("message from worker " + new { SharedField });

                            // how do we signal our work is done?
                        }
                    )
                );

                tt.InternalBeforeStart =
                    w =>
                    {
                        // how are we supposed to get data back from the worker?

                        var fromWorker = w.createMessageChannel(Worker.current);
                        w.setSharedProperty("fromWorker", fromWorker);

                        fromWorker.channelMessage += e =>
                        {
                            var data = (string)fromWorker.receive();

                            t.appendText(

                                "\n " + new { sw.ElapsedMilliseconds, data }.ToString()

                                );

                        };
                    };

                //Thread.AllocateNamedDataSlot("").

                //Thread.SetData(
                tt.Start("hello world");
            };

        }

    }
}
