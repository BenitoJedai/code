using System.Threading.Tasks;
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
        public ApplicationSprite()
        {
            if (!Worker.current.isPrimordial)
                return;

            // X:\jsc.svn\examples\javascript\async\Test\TaskAsyncTaskRun\TaskAsyncTaskRun\Application.cs

            var t = new TextField
            {
                text = "click me!"
            }.AttachTo(this);


            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Runtime\CompilerServices\AsyncTaskMethodBuilder.cs

            //            BCL needs another method, please define it.
            //Cannot call type without script attribute :
            //System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1[System.String] for System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1[System.String] Create() used at
            //TestAsyncTaskRun.ApplicationSprite.<.ctor > b__3 at offset 0002.
            //If the use of this method is intended, an implementation should be provided with the attribute[Script(Implements = typeof(...)] set.You may have mistyped it.

            t.click +=
                async delegate
            {
                t.text = "click! 500";

                await Task.Run(
                    async delegate
                {


                    return "hello world";
                }
                );


                t.text = "click! 0";

            };

        }

    }
}
