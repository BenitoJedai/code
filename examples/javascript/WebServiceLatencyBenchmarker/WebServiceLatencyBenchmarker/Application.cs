using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebServiceLatencyBenchmarker.HTML.Pages;

namespace WebServiceLatencyBenchmarker
{
    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    internal sealed class Application : ApplicationWebService
    {

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault page)
        {
            @"Hello world".ToDocumentTitle();

            var host = Native.document.location.host;

            WriteLine("Pinging host [" + host + "]:");

            int Counter = 0;

            // look async. does roslyn work for us? it does work.
            Action loop = async delegate
            {
                while (true)
                {
                    var Delay = new Stopwatch();
                    Delay.Start();

                    Counter++;

                    // Send data from JavaScript to the server tier
                    await this.yield();


                    this.elapsed = Delay.ElapsedMilliseconds;
                    var DelayString = "" + Convert.ToInt32(Delay.ElapsedMilliseconds) + "ms";

                    // jsc: this will cause an if block which is not supported just yet
                    //new IHTMLDiv { innerText = "" + DelayString }.AttachToDocument();

                    WriteLine("Reply #" + Counter + " from " + host + " time=" + DelayString);
                }

            };

            loop();


            bool once = false;

            Native.window.onfocus +=
                e =>
                {
                    if (once)
                        return;
                    once = true;
                    Native.document.body.requestFullscreen();
                };
        }

        void WriteLine(string e)
        {
            new IHTMLDiv { innerText = e }.AttachToDocument();
            Native.window.scrollTo(0, Native.document.body.clientHeight);
        }




    }
}
