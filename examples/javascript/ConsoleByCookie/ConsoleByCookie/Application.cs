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
using ConsoleByCookie.Design;
using ConsoleByCookie.HTML.Pages;
using SQLiteWithDataGridView.Library;
using ScriptCoreLib.JavaScript.Runtime;

namespace ConsoleByCookie
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        // "X:\jsc.svn\examples\javascript\EventSourceForWebServiceYield\EventSourceForWebServiceYield.sln"
        // "X:\jsc.svn\examples\javascript\ServerSideEventExperiment\ServerSideEventExperiment.sln"

        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            var f = new ConsoleForm();

            f.InitializeConsoleFormWriter();
            f.Show();

            // shard across browsers in the same session?
            var session = new Cookie("session").DefaultToRandomInt32();
            //Native.Window.localStorage

            var random = new Cookie("random").DefaultToRandomInt32();


            Console.WriteLine("\n Console has been redirected!");
            Console.WriteLine("\n " + new { session = session.IntegerValue.ToString("x8") });

            #region WaitInHandler
            page.WaitInHandler.onclick +=
                delegate
                {
                    page.WaitInHandler.disabled = true;
                    new IXMLHttpRequest(
                        url: "/wait",
                        handler: r =>
                            {
                                page.WaitInHandler.disabled = false;

                            }
                    );
                };
            #endregion

            page.CheckDatabase.onclick +=
                delegate
                {
                    service.CheckDatabase("",
                        Console.WriteLine
                    );

                };

            #region CheckServerForSession
            page.CheckServerForSession.onclick +=
                delegate
                {
                    page.CheckServerForSession.disabled = true;

                    service.CheckServerForSession("" + session.IntegerValue,
                        x =>
                        {
                            Console.WriteLine(x);
                            page.CheckServerForSession.disabled = false;
                        }
                    );
                };
            #endregion

            page.DoLongOperation.onclick +=
                delegate
                {
                    page.DoLongOperation.disabled = true;

                    // can we send IEvent as argument directly?
                    // can we set a field on client side and have
                    // it updated on each call?
                    service.DoLongOperation("" + session.IntegerValue,
                        delegate
                        {
                            page.DoLongOperation.disabled = false;

                        }
                    );
                };

            #region UseCurrentIdToGetOutput
            page.UseCurrentIdToGetOutput.disabled = true;

            // enable while clickonce is active
            page.GetCurrentIdToStartListening.onclick +=
                delegate
                {
                    page.GetCurrentIdToStartListening.disabled = true;

                    service.SelectTransactionKey(
                        "" + session.IntegerValue,
                        id =>
                        {
                            Console.WriteLine(new { id });
                            page.UseCurrentIdToGetOutput.disabled = false;

                            page.UseCurrentIdToGetOutput.onclick +=
                                delegate
                                {
                                    page.UseCurrentIdToGetOutput.disabled = true;



                                    service.SelectContentUpdates(
                                        "" + session.IntegerValue,
                                        id,
                                        y: Console.Write,
                                        ynextid: nextid =>
                                        {
                                            id = nextid;

                                            Console.WriteLine(new { id });
                                            page.UseCurrentIdToGetOutput.disabled = false;
                                        }
                                    );

                                };
                        }
                     );

                };
            #endregion


            #region StartServerSentEvents
            page.StopServerSentEvents.disabled = true;

            page.StartServerSentEvents.onclick +=
                delegate
                {
                    page.StartServerSentEvents.disabled = true;



                    page.StopServerSentEvents.disabled = false;

                    //var n = new XElement("e",
                    //      new XAttribute("session", "" + session.IntegerValue)
                    //);

                    //var q = new StringBuilder();

                    //q.Append("/xml");
                    ////q.Append("?");

                    ////q.Append("e=" + n.ToString());


                    // Web Console(12961): Uncaught ReferenceError: EventSource is not defined at http://192.168.1.100:14690/view-source:46617
                    var s = new EventSource("/stream");

                    s["SystemConsoleOut"] =
                        e =>
                        {
                            //Console.WriteLine(new { SystemConsoleOut = new { e.lastEventId, e.data } });
                            Console.Write(((string)e.data)
                                .Replace("\\n", "\n")
                                .Replace("\\r", "\r")
                                );
                        };

                    s.onerror +=
                       e =>
                       {

                           //Console.WriteLine(new { onerror = new { s.readyState } });

                           if (s.readyState == 2)
                           {
                               // why are we getting that error??

                               s = null;

                               page.StartServerSentEvents.disabled = false;
                               page.StopServerSentEvents.disabled = true;
                           }
                       };

                    page.StopServerSentEvents.onclick +=
                        delegate
                        {
                            if (s == null)
                                return;

                            s.close();
                            s = null;

                            page.StartServerSentEvents.disabled = false;
                            page.StopServerSentEvents.disabled = true;

                        };

                };
            #endregion


            page.ChangeSessionTo.innerText = "session " + session.IntegerValue.ToString("x8");
        }

    }

    public static class X
    {
        public static Cookie DefaultToRandomInt32(this Cookie c)
        {
            if (string.IsNullOrEmpty(c.Value))
            {
                var r = new Random();
                var id = r.Next();

                c.IntegerValue = id;
            }

            return c;
        }
    }
}
