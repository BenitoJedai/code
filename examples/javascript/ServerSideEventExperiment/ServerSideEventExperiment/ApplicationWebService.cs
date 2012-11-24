using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Linq;
using System.Threading;
using System.Xml.Linq;

namespace ServerSideEventExperiment
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(string e, Action<string> y)
        {
            // Send it back to the caller.
            y(e);
        }

        public /* will not be part of web service itself */ void Handler(WebServiceHandler h)
        {
            if (h.Context.Request.Path == "/events")
            {
                h.Context.Response.ContentType = "text/event-stream";

                Thread.Sleep(500);

                h.Context.Response.Write("data: hello 1\n\n");
                Thread.Sleep(500);


                // The default event type is "message".
                h.Context.Response.Write("event: foo\n");
                h.Context.Response.Write("data: bar\n\n");
                Thread.Sleep(500);

                h.Context.Response.Write("data: hello 2\n\n");
                Thread.Sleep(500);

                h.CompleteRequest();
                return;
            }
        }

    }
}
