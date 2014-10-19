using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AsyncWorkerSourceSHA1;
using AsyncWorkerSourceSHA1.Design;
using AsyncWorkerSourceSHA1.HTML.Pages;
using System.Diagnostics;

namespace AsyncWorkerSourceSHA1
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
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201410/20141019
            // X:\jsc.svn\examples\javascript\Test\TestScriptApplicationIntegrity\TestScriptApplicationIntegrity\Application.cs

            new IHTMLButton { "worker sha1(view-source)" }.AttachToDocument().onclick += async delegate
            {
                var sw = Stopwatch.StartNew();

                // X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\INode.Add.cs
                //new IHTMLPre { sw }.AttachToDocument().css[

                var swStopEvent = sw.AsStopEvent();

                new IHTMLPre { sw }.AttachToDocument().css[swStopEvent].style.color = "blue";

                new { }.With(
                    async scope =>
                    {
                        await swStopEvent;

                        new IHTMLPre { "swStopEvent" }.AttachToDocument();
                    }
                );



                // can we do a css conditional already, where when stopwatch stops color changes?
                // in a way its unary op, event and a task.
                // we already have Task.
                // Stopwatch does not have events nor tasks
                // can we translate it?




                await Task.Delay(1000);

                sw.Stop();

                //new IHTMLPre { new { sw.IsRunning } }.AttachToDocument();

            };


        }

    }

    static class X
    {
        public static Task AsStopEvent(this Stopwatch sw)
        {
            var x = new TaskCompletionSource<Stopwatch>();

            new { }.With(
                async scope =>
                {
                    var i = 0;
                    i++;
                    Native.document.title = new { i }.ToString();
                    while (sw.IsRunning)
                    {
                        i++;
                        Native.document.title = "yield " + new { i };
                        // will it wait a frame?
                        // X:\jsc.svn\examples\javascript\Test\TestAsyncAssignArrayToEnumerable\TestAsyncAssignArrayToEnumerable\Application.cs
                        //await Task.Yield();
                        //await Native.window.async.onframe;
                        await Task.Delay(1);


                        Native.document.title = "continue " + new { i };
                    }
                    i = -i;

                    Native.document.title = new { i }.ToString();
                    x.SetResult(sw);
                }
            );

            return x.Task;
        }
    }
}
