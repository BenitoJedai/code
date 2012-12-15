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
using ServerSideEventExperiment.Design;
using ServerSideEventExperiment.HTML.Pages;
using ScriptCoreLib.JavaScript.Runtime;

namespace ServerSideEventExperiment
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
            //Native.Document.oner
            // http://www.w3schools.com/html/html5_serversentevents.asp
            // http://stackoverflow.com/questions/5195452/websockets-vs-server-sent-events-eventsource

            new Cookie("xfoo").Value = "foo";

            // Uncaught ReferenceError: EventSource is not defined at http://192.168.1.104:29568/view-source:28282

            var s = new EventSource(url: "/foo?bar=2");

            s.onopen +=
                e =>
                {
                    new IHTMLPre { innerText = "open" }.AttachToDocument();
                };

            s.onerror +=
                e =>
                {
                    new IHTMLPre { innerText = "error " + new { s.readyState } }.AttachToDocument();

                    //s.close();
                };

            s.onmessage +=
                e =>
                {
                    var now = DateTime.Now;

                    new IHTMLPre { innerText = now + " message " + e.data + " " + e.lastEventId }.AttachToDocument();
                };

            //            script: error JSC1000:
            //error:
            //  statement cannot be a load instruction (or is it a bug?)
            //  [0x00ad] ldloc.1    +1 -0

            s["foo"] =
                e =>
                {

                    var m = (MessageEvent)e;

                    new IHTMLPre { innerText = "foo " + m.data }.AttachToDocument();
                };

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }


}
