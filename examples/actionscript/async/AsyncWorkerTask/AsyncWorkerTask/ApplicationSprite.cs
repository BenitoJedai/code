using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.system;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.Extensions;
using System.Threading.Tasks;

namespace AsyncWorkerTask
{
    public sealed class ApplicationSprite : Sprite
    {
        // X:\jsc.svn\examples\actionscript\FlashWorkerExperiment\FlashWorkerExperiment\ApplicationSprite.cs
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201403/20140301

        public ApplicationSprite()
        {
            // when can we do this?

            //BCL needs another method, please define it.
            //Cannot call type without script attribute :
            //System.Threading.Tasks.Task for System.Threading.Tasks.TaskFactory get_Factory() used at
            //AsyncWorkerTask.ApplicationSprite..ctor at offset 0010.

            var a = Task.Factory.StartNew(
                state: new { goo = "foo " },
                function: scope =>
                {



                    // notice that jsc might need to inject serialization logic here
                    return "hello " + new { scope.goo };
                }
            );

            a.ContinueWithResult(
                r =>
                {
                    var t = new TextField
                    {
                        text = new
                        {
                            r,

                            WorkerDomain = WorkerDomain.isSupported,
                            Worker = Worker.isSupported,
                            Worker.current.isPrimordial,

                            this.loaderInfo.bytes.length
                        }.ToString(),

                        autoSize = TextFieldAutoSize.LEFT
                    };

                    t.AttachTo(this);
                }
            );


        }

    }
}

