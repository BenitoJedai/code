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
using AsyncWorkerWebServiceProgressEventStream;
using AsyncWorkerWebServiceProgressEventStream.Design;
using AsyncWorkerWebServiceProgressEventStream.HTML.Pages;
using System.Threading.Tasks;
using System.Threading;

namespace AsyncWorkerWebServiceProgressEventStream
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            new IHTMLButton { innerText = "do it" }.AttachToDocument().WhenClicked(
                async btn =>
                {
                    // um click happened!

                    var done = new TaskCompletionSource<string>();


                    Task.Factory.StartNewWithProgress(
                        new { input = "input", output = "output" },

                        function:
                            scope =>
                            {
                                var p = scope.Item1;
                                var state = scope.Item2;

                                p.Report(
                                    new { input = "", output = "starting..." }
                                );

                                Thread.Sleep(900);

                                var service = new ApplicationWebService();

#if future
                                service.NFCTagDiscovered +=
                                    data =>
                                    {
                                        // hello
                                    };
#endif


                                service.add_NFCTagDiscovered(
                                    data =>
                                    {
                                        // worker-task was made aware. time to make GUI aware!

                                        p.Report(
                                            new { input = "", output = "! NFCTagDiscovered..." + new { data } }
                                        );
                                    }
                                );


                                //p.Report(
                                //    new { input = "", output = "hi" }
                                //);


                                return state;
                            },

                        progress:
                            state =>
                            {
                                new IHTMLPre { innerText = new { state.output }.ToString() }.AttachToDocument();


                                //if (state.output == "hi")
                                //{
                                //    done.SetResult(state.output);
                                //}
                            }
                    );

                    // this is one way, will never complete..
                    // if device goes missing, then what?
                    await done.Task;
                }
            );


        }

    }

    public static class X
    {
        public static void add_NFCTagDiscovered(this ApplicationWebService service, Action<string> y)
        {
            var s = new EventSource();

            s["y"] = a =>
            {


                var data = a.data.ToString()
                    .Replace("\\r", "\r")
                    .Replace("\\n", "\n");


                y(data);
            };

            s.onerror +=
                delegate
                {
                    s.close();
                };

        }
    }
}
