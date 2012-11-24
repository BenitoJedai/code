using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Linq;
using System.Threading;
using System.Xml.Linq;

namespace AccelerometerServerEvents
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
            // http://www.sitepoint.com/server-sent-events/
            //Object '/d07dea9a_2384_49f8_8c01_270582d093dc/rwqwygw27obp9zsviyqgjhqt_27.rem' has been disconnected or does not exist at the server.

            // http://www.w3.org/Protocols/HTTP/HTRQ_Headers.html
            var Accepts = h.Context.Request.Headers["Accept"];

            if (Accepts != null)
                if (Accepts.Contains("text/event-stream"))
                {
                    h.Context.Response.ContentType = "text/event-stream";


                    Action<XElement> data =
                        xml =>
                        {
                            h.Context.Response.Write("data: " + xml.ToString() + "\n\n");

                        };

                    data(
                        new XElement("onaccelerometer",
                            new XAttribute("x", "0.0"),
                            new XAttribute("y", "0.0"),
                            new XAttribute("z", "0.0")
                        )
                    );

                    Thread.Sleep(1000);

                    h.CompleteRequest();
                    return;
                }
        }
    }
}
