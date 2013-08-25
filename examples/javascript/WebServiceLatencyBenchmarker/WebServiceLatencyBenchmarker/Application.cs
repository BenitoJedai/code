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
using WebServiceLatencyBenchmarker.HTML.Pages;

namespace WebServiceLatencyBenchmarker
{
    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    internal sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault page)
        {
            @"Hello world".ToDocumentTitle();
            var host = Native.Document.location.host;

            WriteLine("Pinging host [" + host + "]:");

            Action Do = null;

            Do = delegate
            {
                Continue(host,
                    delegate
                    {
                        Do();
                    }
                );
            };

            Do();

            bool once = false;

            Native.window.onfocus +=
                e =>
                {
                    if (once)
                        return;
                    once = true;
                    Native.Document.body.requestFullscreen();
                };
        }

        void WriteLine(string e)
        {
            new IHTMLDiv { innerText = e }.AttachToDocument();
            Native.window.scrollTo(0, Native.Document.body.clientHeight);
        }

        int Counter = 0;

        void Continue(string host, Action done)
        {
            var Now = DateTime.Now;

            Counter++;

            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value =>
                {
                    var Later = DateTime.Now;

                    var Delay = Later - Now;
                    var DelayString = "" + Convert.ToInt32(Delay.TotalMilliseconds) + "ms";

                    // jsc: this will cause an if block which is not supported just yet
                    // new IHTMLDiv { innerText = "" + NowString }.AttachToDocument();

                    WriteLine("Reply #" + Counter + " from " + host + " time=" + DelayString);

                    if (done != null)
                        done();
                }
            );
        }

    }
}
