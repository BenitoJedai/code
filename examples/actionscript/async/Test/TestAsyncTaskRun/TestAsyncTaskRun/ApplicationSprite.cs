using System.Threading.Tasks;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.Extensions;

namespace TestAsyncTaskRun
{
    public sealed class ApplicationSprite : Sprite
    {
        public ApplicationSprite()
        {
            // X:\jsc.svn\examples\javascript\async\Test\TaskAsyncTaskRun\TaskAsyncTaskRun\Application.cs

            var t = new TextField
            {
                text = "click me!"
            }.AttachTo(this);


            t.click +=
                //async
                delegate
            {
                t.text = "click! 500";

                //await Task.Delay(500);

                    
                var timer = new Timer(500);

                //timer.timerComplete +=
                timer.timer +=
                    delegate
                {
                    t.text = "click! 0";
                };

                timer.start();


                //500.At
                //t.text = "click!";

            };

        }

    }
}
