using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.Extensions;

namespace TestTaskDelay
{
    public sealed class ApplicationSprite : Sprite
    {
        // X:\jsc.svn\examples\java\Test\TestGenericByRefThis\TestGenericByRefThis\Class1.cs
        // X:\jsc.svn\examples\javascript\Test\TestByRefLdarg0\TestByRefLdarg0\Program.cs

        //{ errorID = 1006, message = Error #1006: GetResult_4ebbe596_06000cdb is not a function., StackTrace = TypeError: Error #1006: GetResult_4ebbe596_06000cdb is not a function.
        //	at TestTaskDelay::ApplicationSprite____ctor_b__4_d__0__MoveNext_06000024$/_007c__ldloca_s_try_583b1500_06000044()

        //__Console.WriteLine_4ebbe596_0600125a("TestTaskDelay.ApplicationSprite+<<_ctor>b__4>d__0 <007c> ldloca.s");
        //    ref_arg3.GetResult_4ebbe596_06000cdb();

        //    V:\web\TestTaskDelay\ApplicationSprite___c__DisplayClass0____ctor_b__2_d__0.as(14): col: 24 Warning: No constructor function was specified for class ApplicationSprite___c__DisplayClass0____ctor_b__2_d__0.
        //public final class ApplicationSprite___c__DisplayClass0____ctor_b__2_d__0 implements __IAsyncStateMachine
        //                   ^

        class __OutWriter : TextWriter
        {
            public Action<string> AtWriteLine;

            static StringBuilder WriteLinePending = new StringBuilder();


            public override void Write(string value)
            {
                WriteLinePending.Append(value);
            }



            public override void WriteLine(object value)
            {
                var xError = value as Error;
                if (xError != null)
                {

                    this.WriteLine(
                        new
                    {
                        xError.errorID,
                        xError.message,
                        StackTrace = xError.getStackTrace()
                    }.ToString()
                    );
                    return;
                }

                this.WriteLine(
                    new { value }.ToString()
                );

            }


            public override void WriteLine(string value)
            {
                var x = WriteLinePending.ToString();

                if (x.Length > 0)
                    WriteLinePending = new StringBuilder();

                var n = x + value;

                if (AtWriteLine != null)
                    AtWriteLine(n);
            }

            public override Encoding Encoding
            {
                get { return Encoding.UTF8; }
            }
        }

        // can we stream console to html?
        //public Action<string> AtWriteLine;
        public event Action<string> AtWriteLine;

        public ApplicationSprite()
        {
            // http://help.adobe.com/en_US/ActionScript/3.0_ProgrammingAS3/WS5b3ccc516d4fbf351e63e3d118a9b90204-7e15.html
            //this.stage.opaqueBackground = 0xff;
            //this.opaqueBackground = 0xff;

            // X:\jsc.svn\examples\actionscript\async\Test\TestAsyncTaskRun\TestAsyncTaskRun\ApplicationSprite.cs



            var t = new TextField
            {
                text = "before"
            }.AttachTo(this);


            Console.SetOut(
                new __OutWriter
            {
                AtWriteLine = x =>
                {

                    t.text = x;


                    if (AtWriteLine != null) AtWriteLine(x);
                }
            }
            );

            // C:\Windows\system32\Macromed\Flash\NPSWF64_15_0_0_189.dll

            Console.WriteLine(@"hello!");

            t.click += delegate
            {
                Console.WriteLine(@"see C:\Users\arvo\AppData\Roaming\Macromedia\Flash Player\Logs");



                //            null
                //at TestTaskDelay::ApplicationSprite$/ __ctor_b__2_583b1500_06000002()

                //throw null;




                new { t }.With(
                    async scope =>
                 {

                     //t.text = "after 1";
                     Console.WriteLine("enter async");

                     //                     enter async
                     //__AsyncVoidMethodBuilder.SetException { exception =  }

                     // this does not yet work does it.
                     await Task.Delay(500);

                     //var a = Task.Delay(500).GetAwaiter();

                     //a.OnCompleted(
                     //    delegate
                     //{

                     //.ContinueWith(
                     //    task =>
                     //   {
                     //t.text = "after 500";
                     Console.WriteLine("exit async");

                     //   }
                     //);

                     //}
                     //    );



                }
                );

            };

        }

    }
}
