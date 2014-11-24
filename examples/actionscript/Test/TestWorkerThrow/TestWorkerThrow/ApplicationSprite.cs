using System;
using System.Diagnostics;
using System.Threading;
using ScriptCoreLib.ActionScript.BCLImplementation.System;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.Extensions;

namespace TestWorkerThrow
{
    public sealed class ApplicationSprite : Sprite
    {
        public ApplicationSprite()
        {

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

            t.click += delegate
            {
                // X:\jsc.svn\examples\actionscript\Test\TestWorkerConsole\TestWorkerConsole\ApplicationSprite.cs

                var sw = Stopwatch.StartNew();

                Console.WriteLine("enter click " + new { Thread.CurrentThread.ManagedThreadId, sw.ElapsedMilliseconds });

                new Thread(delegate (object arg0)
                {
                    //Console.WriteLine("in thread " + new { Thread.CurrentThread.ManagedThreadId, sw.ElapsedMilliseconds });



                    //try
                    //{
                    // 1706ms catch {{ err = TypeError: Error #1009: Cannot access a property or method of a null object reference. }}

                    //ApplicationDomain
                    //__Thread.InternalWorkerSprite.loaderInfo.uncaughtErrorEvents.uncaughtError +=
                    //    e =>
                    //        {
                    //            e.preventDefault();

                    //            Console.WriteLine("catch " + new { e.errorID, e.error });
                    //        };

                    Console.WriteLine("what happens if we are to crash? will it be repoted in console?");

                    throw null;
                    //ThrowIt();
                    //return;
                    //}
                    //catch (Exception err)
                    //{
                    //    // 1457ms catch {{ err = Error }}
                    //    Console.WriteLine("catch " + new { err });
                    //}
                    Console.WriteLine("done");
                }
                ).Start(null);

                Console.WriteLine("exit click " + new { x, Thread.CurrentThread.ManagedThreadId, sw.ElapsedMilliseconds });
            };


        }

        private static void ThrowIt()
        {
            // 1414ms catch {{ err = Error: throw null }}

            throw null;
            //             throw new Error("throw null");
            //throw new Exception("throw null");
        }
    }
}
