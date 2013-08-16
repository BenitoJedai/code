using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Xml.Linq;

namespace EventSourceForWebServiceYield
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed partial class ApplicationWebService
    {
        // see also:
        // http://www.yiiframework.com/wiki/329/real-time-display-of-server-push-data-using-server-sent-events-sse/
        // http://dsheiko.com/weblog/websockets-vs-sse-vs-long-polling

        public void Invoke(XElement e, Action<XElement> y)
        {
            for (int i = 0; i < 8; i++)
            {
                var now = DateTime.Now;

                var n = new XElement("server",
                    new XAttribute("value", "" + now),
                    XElement.Parse(e.ToString())
                );

                Console.WriteLine(n);

                y(n);

                Thread.Sleep(200);
            }
        }

    }

    #region Invoke Special

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
                    var _e_xml = h.Context.Request.RawUrl
                        .SkipUntilLastOrEmpty("?")
                        .SkipUntilOrEmpty("e=")
                        .TakeUntilIfAny("&");

                    var _e_xml_decoded = HttpUtility.UrlDecode(_e_xml);

                    var _e = XElement.Parse(_e_xml_decoded);

                    this.Invoke(
                        _e,
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

    public static class X
    {
        public static void InvokeSpecial(this ApplicationWebService service, XElement e, Action<XElement> y)
        {
            var q = new StringBuilder();

            //q.Append("/event-stream");
            q.Append("?");

            q.Append("e=" + e.ToString());

            var s = new EventSource(q.ToString());

            s["y"] = a =>
            {


                var data = a.data.ToString()
                    .Replace("\\r", "\r")
                    .Replace("\\n", "\n");

                Console.WriteLine(new { data });

                var _y = XElement.Parse(data);

                y(_y);
            };

            s.onerror +=
                delegate
                {
                    s.close();
                };

        }

        public static void InvokeSpecialString(this ApplicationWebService service, string e, Action<string> y)
        {
            var q = new StringBuilder();

            //q.Append("/event-stream");
            q.Append("?");

            q.Append("e=" + e);

            var s = new EventSource(q.ToString());

            s["y"] = a =>
            {


                var data = a.data.ToString()
                    .Replace("\\r", "\r")
                    .Replace("\\n", "\n");

                Console.WriteLine(new { data });

                //var _y = XElement.Parse(data);

                y(data);
            };

            s.onerror +=
                delegate
                {
                    s.close();
                };

        }
    }
    #endregion


}
