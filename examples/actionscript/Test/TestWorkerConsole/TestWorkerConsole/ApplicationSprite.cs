using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.Extensions;

namespace TestWorkerConsole
{
    public sealed class ApplicationSprite : Sprite
    {
        public ApplicationSprite()
        {
            //X:\jsc.svn\examples\actionscript\air\AIRThreadedSoundAsyncLoop\AIRThreadedSoundAsyncLoop\ApplicationSprite.cs


            var t = new TextField
            {
                multiline = true,

                //backgroundColor = 0xff000000u,
                //textColor = 0xffffffffu,

                autoSize = TextFieldAutoSize.LEFT,

                text = "..."

                // X:\jsc.svn\examples\actionscript\Test\TestWorkerConsole\TestWorkerConsole\ApplicationSprite.cs
            }.AttachToSprite().AsConsole();

            Console.WriteLine(new { Thread.CurrentThread.ManagedThreadId });

            t.click += async delegate
            {
                var sw = Stopwatch.StartNew();

                Console.WriteLine("enter click " + new { Thread.CurrentThread.ManagedThreadId, sw.ElapsedMilliseconds });

                // http://blogs.msdn.com/b/dotnet/archive/2012/06/06/async-in-4-5-enabling-progress-and-cancellation-in-async-apis.aspx
                // Progress
                // Task of Click?
                // Dispatcher

                var x = await Task.Run(
                    async delegate
                {
                    Console.WriteLine("threaded click " + new { Thread.CurrentThread.ManagedThreadId });

                    // if we were to run physic in worker,
                    // how would we update gpu?

                    await Task.Delay(500);


                    return "done";
                }
                );

                Console.WriteLine("exit click " + new { x, Thread.CurrentThread.ManagedThreadId, sw.ElapsedMilliseconds });
            };



        }

    }
}
