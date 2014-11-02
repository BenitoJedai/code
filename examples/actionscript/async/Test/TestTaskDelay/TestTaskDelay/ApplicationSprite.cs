using System.Threading.Tasks;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.Extensions;

namespace TestTaskDelay
{
    public sealed class ApplicationSprite : Sprite
    {
        //    V:\web\TestTaskDelay\ApplicationSprite___c__DisplayClass0____ctor_b__2_d__0.as(14): col: 24 Warning: No constructor function was specified for class ApplicationSprite___c__DisplayClass0____ctor_b__2_d__0.
        //public final class ApplicationSprite___c__DisplayClass0____ctor_b__2_d__0 implements __IAsyncStateMachine
        //                   ^

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



            new { t }.With(
                async scope =>
                 {

                     t.text = "after 1";

                     // this does not yet work does it.
                     await Task.Delay(500);

                     //var a = Task.Delay(500).GetAwaiter();

                     //a.OnCompleted(
                     //    delegate
                     //{

                     //.ContinueWith(
                     //    task =>
                     //   {
                     t.text = "after 500";
                     //   }
                     //);

                     //}
                     //    );



            }
            );

        }

    }
}
