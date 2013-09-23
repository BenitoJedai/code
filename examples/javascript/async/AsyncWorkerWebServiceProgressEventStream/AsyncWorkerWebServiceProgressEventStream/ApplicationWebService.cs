using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Linq;
using System.Threading;
using System.Xml.Linq;

namespace AsyncWorkerWebServiceProgressEventStream
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed partial class ApplicationWebService
    {
        // is ths instance always recreated on a different thread?

        // remvoe should be called as soon as client disconnects?
        // can we use this event as a component default event?
        public void add_NFCTagDiscoveredInternal(Action<string> value)
        {
            // Send it back to the caller.

            Thread.Sleep(555);

            value("hi! id=6 " + new { Thread.CurrentThread.ManagedThreadId });


            Thread.Sleep(555);

            value("hi! id=7 " + new { Thread.CurrentThread.ManagedThreadId });
        }

        // jsc how long we can do just this?
#if future
        internal event Action<string> NFCTagDiscovered;
#endif

    }


    partial class ApplicationWebService
    {
        public /* will not be part of web service itself */ void Handler(WebServiceHandler h)
        {
            var Accepts = h.Context.Request.Headers["Accept"];

            //if (h.Context.Request.Path == "/xml")

            if (Accepts != null)
                if (Accepts.Contains("text/event-stream"))
                {
                    h.Context.Response.ContentType = "text/event-stream";


                    // A potentially dangerous Request.QueryString value was detected from the client (e="<client value="15.12...").
                    //var _e_xml = h.Context.Request.RawUrl
                    //    .SkipUntilLastOrEmpty("?")
                    //    .SkipUntilOrEmpty("e=")
                    //    .TakeUntilIfAny("&");

                    //var _e_xml_decoded = HttpUtility.UrlDecode(_e_xml);

                    //var _e = XElement.Parse(_e_xml_decoded);

                    this.add_NFCTagDiscoveredInternal(
                        y =>
                        {
                            var _y = y.ToString()
                                .Replace("\n", "\\n")
                                .Replace("\r", "\\r");


                            h.Context.Response.Write("event: y\n");
                            h.Context.Response.Write("data: " + _y + "\n\n");
                            h.Context.Response.Flush();
                        }
                    );

                    h.CompleteRequest();
                    return;
                }
        }
    }

}
