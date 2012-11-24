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
using AccelerometerServerEvents.Design;
using AccelerometerServerEvents.HTML.Pages;

namespace AccelerometerServerEvents
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
            // this is like a ComponentModel timer where handler can raise events
            var s = new EventSource();

            s.onmessage +=
                e =>
                {
                    var xml = XElement.Parse((string)e.data);

                    page.Content.Clear();

                    new IHTMLPre { innerText = xml.ToString() }.AttachTo(page.Content);
                };
        }

    }
}
