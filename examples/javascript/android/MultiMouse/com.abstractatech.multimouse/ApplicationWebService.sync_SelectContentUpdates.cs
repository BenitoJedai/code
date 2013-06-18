using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;

namespace com.abstractatech.multimouse
{
    partial class ApplicationWebService
    {
        public /* will not be part of web service itself */ void Handler(WebServiceHandler h)
        {
            // this is on the server now!


            // via X:\jsc.svn\examples\javascript\ConsoleByCookie\ConsoleByCookie\ApplicationWebService.cs

            var Accepts = h.Context.Request.Headers["Accept"];


            if (Accepts != null)
                if (Accepts.Contains("text/event-stream"))
                {
                    Console.WriteLine("enter stream");

                    h.Context.Response.ContentType = "text/event-stream";

                    // do we need this?
                    h.Context.Response.Cache.SetCacheability(HttpCacheability.NoCache);

                    var header_id = h.Context.Request.Headers["Last-Event-ID"];

                    if (header_id == null)
                        header_id = h.Context.Request.QueryString["id"];

                    var id = 0;

                    if (!string.IsNullOrEmpty(header_id))
                    {
                        //__ConsoleToDatabaseWriter.InternalWrite("Continue " + new { session, header_id }.ToString() + Environment.NewLine);

                        //id = int.Parse(XElement.Parse(header_id).Attribute("id").Value);
                        id = int.Parse(header_id);
                    }
                    else
                    {



                        ////sync.SelectTransaction(
                        ////     nextid =>
                        ////     {
                        ////         id = (int)nextid;

                        ////         //var xml = new XElement("e",
                        ////         //    new XAttribute("id", id)
                        ////         //);
                        ////         //__ConsoleToDatabaseWriter.InternalWrite("Reset To " + new { session, id }.ToString() + Environment.NewLine);

                        ////         h.Context.Response.Write("id: " + id + "\n\n");
                        ////         //h.Context.Response.Write("event: SystemConsoleOut\n");
                        ////         h.Context.Response.Write("data: reset to " + new { id } + " \n\n");
                        ////         h.Context.Response.Flush();

                        ////     }
                        //// );
                    }

                    int retry = 1000 / 30;

                    // max?
                    this.sync_SelectContentUpdates_timeout = 30000;
                    this.sync_SelectContentUpdates_waitmin = 10;
                    this.sync_SelectContentUpdates_waitrandom = 50;
                    this.sync_SelectContentUpdates("" + id,
                        yield: value =>
                        {
                            h.Context.Response.Write("event: yield\n");
                            h.Context.Response.Write("data: " +
                                value.ToString().Replace("\r", "\\r").Replace("\n", "\\n") + "\n\n");
                            h.Context.Response.Flush();

                            //Console.WriteLine("yield");

                        },
                        yield_last_id: value =>
                        {
                            id = int.Parse(value);
                            h.Context.Response.Write("id: " + id + "\n\n");
                            h.Context.Response.Flush();
                        }
                    );

                    // notice AppEngine wont support gzip neither server sent events :)
                    // should jsc adobt channel api?
                    // 500 is done. anything else to do?

                    #region prevent error 2, required if we did not send any other events
                    h.Context.Response.Write("retry: " + retry + "\n\n");
                    h.Context.Response.Write("data: retry later\n\n");
                    h.Context.Response.Flush();
                    #endregion


                    h.CompleteRequest();

                    Console.WriteLine("exit stream");

                    return;
                }
        }
    }

    public static class ApplicationWebServiceExtensionsForClient
    {
        public static void sync_SelectContentUpdates_EventStream(this ApplicationWebService service, string last_id, Action<XElement> yield, Action<string> yield_last_id)
        {
            // this is on the client now!

            // some clients like IE do not support EventSource!
            // neither does AppEngine:P revert to iframe?


            // jsc might want to look at the features
            // an app is using
            // does the browser support svg, EventSource
            // does the server support event-stream?
            // if not show alternative app or error page
            // IE does not have pointerlock
            // android 2.2 stock browser has no svg!

            dynamic w = Native.Window;
            IFunction EventSource = w.EventSource;

            if (EventSource == null)
            {
                // fallback
                service.sync_SelectContentUpdates(last_id, yield, yield_last_id);
                return;
            }

            #region EventSource
            //Console.WriteLine("enter EventSource");
            var s = new EventSource("/ApplicationWebService.sync.SelectContentUpdates?id=" + last_id);

            var id = last_id;

            s["yield"] =
                e =>
                {
                    id = e.lastEventId;

                    //Console.WriteLine(new { SystemConsoleOut = new { e.lastEventId, e.data } });
                    var value = (((string)e.data)
                        .Replace("\\n", "\n")
                        .Replace("\\r", "\r")
                        );

                    yield(XElement.Parse(value));
                };

            s.onmessage +=
                 e =>
                 {
                     id = e.lastEventId;
                 };

            s.onerror +=
                e =>
                {
                    //Console.WriteLine("exit EventSource " + new { id, s.readyState });
                    s.close();

                    yield_last_id(id);
                };
            #endregion




        }
    }
}
