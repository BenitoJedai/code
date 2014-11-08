using System.Diagnostics;
using System.Threading;
using ScriptCoreLib.ActionScript.BCLImplementation.System;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.system;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.Extensions;

namespace TestThreadStartInternalWorkerInvoke
{
    public sealed class ApplicationSprite : Sprite
    {
        // are we to share all static string fields in AIR as we do in chrome?
        public static string SharedField;


        public ApplicationSprite()
        {
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
                            // how can we report to the UI thread?

                            var nn = Stopwatch.StartNew();


                            int i = 0;

                            // keep core2 buzy for a while to be noticed on the task manager
                            while (nn.ElapsedMilliseconds < 10000)
                            {
                                SharedField = new { data, i, nn.ElapsedMilliseconds }.ToString();

                                i++;
                            }



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
