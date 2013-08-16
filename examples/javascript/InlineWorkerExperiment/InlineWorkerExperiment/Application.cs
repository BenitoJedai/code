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
using InlineWorkerExperiment;
using InlineWorkerExperiment.Design;
using InlineWorkerExperiment.HTML.Pages;

namespace InlineWorkerExperiment
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
            Native.worker.With(
                worker =>
                {
                    // fake inline
                    worker.postMessage("xxx fake");
                }
            );

            Native.window.With(
                window =>
                {
                    // what about multiple?
                    // what about dynamic handler selector?

                    var ww = new Worker(
                        (DedicatedWorkerGlobalScope worker) =>
                        {
                            // running in worker context. cannot talk to outer scope yet.

                            worker.postMessage("xxx");
                        }
                    );

                    // ww.onmessage: { data = xxx fake }
                    ww.onmessage +=
                        e =>
                        {
                            Console.WriteLine("ww.onmessage: " + new { e.data });
                        };
                }
            );

        }

    }
}
