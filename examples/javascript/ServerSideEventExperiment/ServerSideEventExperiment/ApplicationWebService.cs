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

        // /WebServiceType.Method{event1, event2}

        public /* will not be part of web service itself */ void Handler(WebServiceHandler h)
        {
            // webrtc!

            // http://www.sitepoint.com/server-sent-events/
            //Object '/d07dea9a_2384_49f8_8c01_270582d093dc/rwqwygw27obp9zsviyqgjhqt_27.rem' has been disconnected or does not exist at the server.

            // http://www.w3.org/Protocols/HTTP/HTRQ_Headers.html
            var Accepts = h.Context.Request.Headers["Accept"];

            if (Accepts != null)
                if (Accepts.Contains("text/event-stream"))
                //if (h.Context.Request.AcceptTypes.Contains("text/event-stream"))
                //if (h.Context.Request.Path == "/events")
                {
                    Console.WriteLine(new { h.Context.Request.Path });


                    if (h.Context.Request.Path == "/foo")
                    {
                        h.Context.Response.Redirect("/xoo", true);
                        h.CompleteRequest();
                        return;
                    }

                    var xfoo = h.Context.Request.Headers["xfoo"];

                    var id = h.Context.Request.Headers["Last-Event-ID"];

                    Console.WriteLine(new { id });

                    h.Context.Response.ContentType = "text/event-stream";

                    //                data: The information to be sent.
                    //event: The type of event being dispatched.
                    //id: An identifier for the event to be used when the client reconnects.
                    //retry: How many milliseconds should lapse before the browser attempts to reconnect to the URL.

                    var now = DateTime.Now;

                    // its like a continuation of the last value we already sent.

                    var xml = new XElement("id",
                        new XAttribute("Ticks", now.Ticks),
                        new XAttribute("Path", h.Context.Request.Path)
                    );

                    h.Context.Response.Write("id: " + xml.ToString() + "\n\n");

                    Thread.Sleep(2000);

                    h.Context.Response.Write("data: hello 1\n\n");
                    h.Context.Response.Flush();
                    Thread.Sleep(2000);


                    // The default event type is "message".
                    h.Context.Response.Write("event: foo\n");
                    h.Context.Response.Write("data: bar\n\n");
                    h.Context.Response.Flush();
                    Thread.Sleep(2000);

                    h.Context.Response.Write("data: hello 2\n\n");
                    h.Context.Response.Flush();
                    Thread.Sleep(2000);

                    h.CompleteRequest();
                    return;
                }
        }

    }
}
