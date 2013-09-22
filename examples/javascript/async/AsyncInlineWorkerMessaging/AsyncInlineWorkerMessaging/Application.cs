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
using AsyncInlineWorkerMessaging;
using AsyncInlineWorkerMessaging.Design;
using AsyncInlineWorkerMessaging.HTML.Pages;

namespace AsyncInlineWorkerMessaging
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            new Worker(
                worker =>
                {
                    // running in worker context. cannot talk to outer scope yet.


                    worker.onmessage +=
                        e =>
                        {


                            e.ports.WithEach(
                                async port =>
                                {
                                    port.postMessage(
                                        new { x = 2, e.data }
                                    );
                                }
                            );
                        };
                }
            ).With(
                async ww =>
                {

                    var x = await ww.postMessageAsync(new { hello = "world" });

                    new IHTMLPre { innerText = new { goo = "bar", x.data }.ToString() }.AttachToDocument();
                }
            );
        }

    }
}
