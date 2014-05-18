using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;

using ScriptCoreLib.JavaScript.DOM.HTML;

using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using AsyncImageTask;
using AsyncImageTask.Design;
using AsyncImageTask.HTML.Pages;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace AsyncImageTask
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {

            new IHTMLButton("load it").AttachToDocument().WhenClicked(
                async btn =>
                {
                    // slow it down
                    await Task.Delay(333);

                    var i = new HTML.Images.FromAssets.jsc();


                    //Console.WriteLine(new { i.complete });
                    //var y = new TaskCompletionSource<IHTMLImage>();
                    //i.InvokeOnComplete(y.SetResult);

                    i.AttachToDocument();
                    Console.WriteLine(new { i.complete });

                    //await y.Task;
                    // Error	1	'System.Runtime.CompilerServices.INotifyCompletion' does not contain a definition for 'IsCompleted'	X:\jsc.svn\examples\javascript\AsyncImageTask\AsyncImageTask\Application.cs	55	21	AsyncImageTask



                    //await j;

                    // reloading visual studio helped here!
                    //IHTMLImageAsyncExtensions.

                    // is this the culprit?
                    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140518-1/roslyn

                    await i;
                    //await i.async.onl;


                    //Native.window.performance.getEntries().WithEach(
                    //    e =>
                    //    {
                    //        new IHTMLPre { new { e.name, e.entryType, e.duration, e.startTime } }.AttachToDocument();
                    //    }
                    //);
                }
            );
        }

    }
}


