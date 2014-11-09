using System.Threading.Tasks;
using System.Diagnostics;
using ScriptCoreLib.ActionScript.BCLImplementation.System;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.system;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.Extensions;

namespace TestAsyncTaskRun
{
    public sealed class ApplicationSprite : Sprite
    {
        // start sharing  field once both sides have showed interest in it?
        public static string SharedField;


        public ApplicationSprite()
        {
            if (!Worker.current.isPrimordial)
                return;

            // X:\jsc.svn\examples\javascript\async\Test\TaskAsyncTaskRun\TaskAsyncTaskRun\Application.cs

            var t = new TextField
            {
                multiline = true,
                text = new
                {
                    __Thread.InternalPrimordialSprite,
                    this.loaderInfo.bytes.length,
                }.ToString(),
                autoSize = TextFieldAutoSize.LEFT
            }.AttachTo(this);


            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Runtime\CompilerServices\AsyncTaskMethodBuilder.cs
            // X:\jsc.svn\examples\actionscript\async\AsyncWorkerTask\AsyncWorkerTask\ApplicationSprite.cs


            t.click +=
                 delegate
            {
                t.text = "click! 500";

                // click!  
                //{ { ElapsedMilliseconds = 3083, xvalue = hello world { { SharedField = { { i = 4094, ElapsedMilliseconds = 29 } } } } } }
                // {{ ElapsedMilliseconds = 83, xvalue = hello world {{ SharedField = {{ i = 4094, ElapsedMilliseconds = 29 }} }} }}
                // {{ ElapsedMilliseconds = 91, xvalue = hello world {{ SharedField = {{ i = 4094, ElapsedMilliseconds = 29 }} }} }}
                // {{ ElapsedMilliseconds = 1192, xvalue = hello world {{ SharedField = {{ i = 65534, ElapsedMilliseconds = 1052 }} }} }}

                var value = Task.Run(
                     delegate
                {
                    var nn = Stopwatch.StartNew();

                    //                NotImplementedException:
                    //                for now AIR supports only static thread starts..
                    //at ScriptCoreLib.ActionScript.BCLImplementation.System::__Task$/ Run_4ebbe596_06000020()
                    //at TestAsyncTaskRun::ApplicationSprite___c__DisplayClass0 / __ctor_b__3_40cabf6e_06000006()

                    //// keep core2 buzy for a while to be noticed on the task manager
                    //while (nn.ElapsedMilliseconds < 10000)

                    //for (int i = 0; i < 0xffff; i++)

                    //for (int z = 0; z < 2; z++)
                    //for (int j = 0; j < 0x4; j++)
                    for (int i = 0; i < 0xffff; i++)
                    {

                        SharedField = new
                        {
                            //data,
                            //z,
                            //j,
                            i,
                            nn.ElapsedMilliseconds
                            //, Thread.CurrentThread.ManagedThreadId 
                        }.ToString();

                        i++;
                    }


                    return Task.FromResult(("hello world " + new { SharedField }));
                }
                );


                //new { }.With(
                //    async delegate
                //{
                //    var ss = Stopwatch.StartNew();

                //    while (!value.IsCompleted)
                //    {
                //        t.text = "click! 500 " + new { ss.ElapsedMilliseconds };

                //        await Task.Delay(1);
                //    }
                //}
                //);

                // click! {{ ElapsedMilliseconds = 5, value = hello world {{ SharedField = {{ i = 4094, ElapsedMilliseconds = 27 }} }} }}

                // why do we need to create a new scope?
                new { value }.With(
                    async scope =>
                    {
                        var sss = Stopwatch.StartNew();


                        while (!value.IsCompleted)
                        {
                            t.text = "click! 500 \n" + new { value.IsCompleted, sss.ElapsedMilliseconds };

                            //this.stage.enterFrame
                            //await Task.Delay(1);
                            await this.async.onframe;
                        }

                        // click! {{ ElapsedMilliseconds = 806, xvalue = hello world {{ SharedField = {{ z = 1, j = 3, i = 4094, ElapsedMilliseconds = 719 }} }} }}

                        var xvalue = await value;

                        // lick! {{ ElapsedMilliseconds = 145, xvalue = hello world {{ SharedField = {{ i = 4094, ElapsedMilliseconds = 100 }} }} }}

                        t.text = "click!  \n" + new { sss.ElapsedMilliseconds, xvalue };
                    }
                );


            };

        }

    }
}
