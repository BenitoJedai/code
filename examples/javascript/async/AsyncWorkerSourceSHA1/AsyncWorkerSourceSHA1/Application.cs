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
using System.Net;
using System.Threading;

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

            new { }.With(
                async scope =>
                {
                    var sw = Stopwatch.StartNew();

                    var div = new IHTMLDiv { }.AttachToDocument();

                    div.style.backgroundColor = "lightgray";
                    div.css.style.borderLeft = "4px solid yellow";
                    div.css.style.transition = "border 500ms linear";

                    div.css.children.style.transition = "color 500ms linear";


                    new IHTMLPre { Native.document.location.protocol, " generating identity: ", sw }.AttachTo(div).css[sw.AsStopEvent()].not.style.color = "red";

                    div.css[sw.AsStopEvent()].style.borderLeft = "4px solid green";

                    // or should the identity be bound by local NFC smart card?
                    var identity = await Native.identity;
                    sw.Stop();

                    new IHTMLPre {
                        new { privateKey = new { identity.publicKey.type, identity.privateKey.extractable } },

                    }.AttachTo(div);


                    new IHTMLPre {
                        new { publicKey= new { identity.publicKey.type, identity.publicKey.extractable } },

                        // {{ privateKey = {{ extractable = false }} }}{{ publicKey = {{ type = public }} }}
                        // if we were to build a cert, can we see the sha1 for local public key?
                    }.AttachTo(div);

                }
            );

            new IHTMLButton { "worker sha1(view-source)" }.AttachToDocument().onclick += async delegate
            {
                // the next experiment can xor sha1 and use pki to add additional in app encryption on top
                // of the Extended Validation SSL schannel
                // app will then be protected against SSL root compromize.


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

                var sha1 = await Task.Run(
                    async delegate
                {
                    // X:\jsc.svn\examples\javascript\Test\TestScriptApplicationIntegrity\TestScriptApplicationIntegrity\Application.cs

                    var c = new WebClient();
                    var bytes = await c.DownloadDataTaskAsync("view-source");

                    var a = new { name = "SHA-1" };
                    var sha1bytes = await Native.crypto.subtle.digestAsync(a, bytes);

                    return new { sha1bytes, Thread.CurrentThread.ManagedThreadId };
                }
                );

                sw.Stop();

                // {{ ManagedThreadId = 1, Length = 20, Worker = {{ ManagedThreadId = 10 }} }}
                new IHTMLPre { new { Thread.CurrentThread.ManagedThreadId, sha1.sha1bytes.Length, Worker = new { sha1.ManagedThreadId } } }.AttachToDocument();
                new IHTMLPre { sha1.sha1bytes }.AttachToDocument();


                //await Task.Delay(1000);


                //new IHTMLPre { new { sw.IsRunning } }.AttachToDocument();

            };


        }

    }

    static class X
    {
        // how can we document/propose/advertise this?
        public static Task AsStopEvent(this Stopwatch sw)
        {
            var x = new TaskCompletionSource<Stopwatch>();

            new { }.With(
                async scope =>
                {
                    // if jsc sees a variable is of no use.
                    // can we take it out?
                    var i = 0;
                    i++;
                    //Native.document.title = new { i }.ToString();
                    while (sw.IsRunning)
                    {
                        i++;
                        //Native.document.title = "yield " + new { i };
                        // will it wait a frame?
                        // X:\jsc.svn\examples\javascript\Test\TestAsyncAssignArrayToEnumerable\TestAsyncAssignArrayToEnumerable\Application.cs
                        //await Task.Yield();
                        //await Native.window.async.onframe;
                        await Task.Delay(1);


                        //Native.document.title = "continue " + new { i };
                    }
                    i = -i;

                    //Native.document.title = new { i }.ToString();
                    x.SetResult(sw);
                }
            );

            return x.Task;
        }
    }
}
