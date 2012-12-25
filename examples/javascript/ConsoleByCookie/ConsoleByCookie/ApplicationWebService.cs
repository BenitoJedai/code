using ConsoleByCookie.Schema;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Xml.Linq;

namespace ConsoleByCookie
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        // can jsc accept parameters to constructor yet?
        // or fields?

        //public ApplicationWebService(int session)
        //{

        //}

        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void CheckServerForSession(string session, Action<string> y)
        {
            new __ConsoleToDatabaseWriter(int.Parse(session));

            var message = new { CheckServerForSession = new { session = int.Parse(session).ToString("x8") } };

            Console.WriteLine("for server: " + message);

            // Send it back to the caller.
            y("for client: " + message.ToString());
        }

        public void DoLongOperation(string session, Action<string> y)
        {
            new __ConsoleToDatabaseWriter(int.Parse(session));

            for (int i = 0; i < 8; i++)
            {
                var message = new { DoLongOperation = new { session = int.Parse(session).ToString("x8"), i } };
                Console.WriteLine(message);
                Thread.Sleep(1000);
            }



            // Send it back to the caller.
            y("done");
        }

        public void SelectTransactionKey(string session, Action<string> y)
        {
            var c = new __ConsoleToDatabaseWriter(int.Parse(session));

            c.data.SelectTransactionKey(
                new SystemConsoleOutQueries.SelectTransaction { session = int.Parse(session) },

                id =>
                {
                    y("" + id);
                }
            );
        }

        public void SelectContentUpdates(string session, string id, Action<string> y, Action<string> ynextid)
        {
            var c = new __ConsoleToDatabaseWriter(int.Parse(session));

            c.data.SelectTransactionKey(
                new SystemConsoleOutQueries.SelectTransaction { session = int.Parse(session) },
                nextid =>
                {
                    c.data.SelectContentUpdates(
                        new SystemConsoleOutQueries.SelectContentUpdates
                        {

                            session = int.Parse(session),
                            id = int.Parse(id),
                            nextid = (int)nextid

                        },
                        r =>
                        {
                            long _id = r.id;
                            string value = r.value;

                            //y(new { _id, value }.ToString());
                            y(value);
                        }
                    );


                    ynextid("" + nextid);
                }
            );


        }

        public /* will not be part of web service itself */ void Handler(WebServiceHandler h)
        {
            var Accepts = h.Context.Request.Headers["Accept"];

            __ConsoleToDatabaseWriter.InternalWrite(
                new { h.Context.Request.Path, Accepts } + Environment.NewLine
            );


            //EventSource's response has a MIME type ("text/plain") that is not "text/event-stream". Aborting the connection.

            if (h.Context.Request.Path == "/wait")
            {
                Thread.Sleep(5000);
                h.CompleteRequest();
                return;
            }

            if (Accepts != null)
                if (Accepts.Contains("text/event-stream"))
                {
                    try
                    {
                        // http://www.w3schools.com/html/html5_serversentevents.asp
                        // http://caniuse.com/eventsource
                        // http://www.w3.org/TR/eventsource/
                        // https://developer.mozilla.org/en-US/docs/Server-sent_events/Using_server-sent_events

                        // http://opensource.apple.com/source/WebCore/WebCore-1640.1/page/EventSource.cpp?txt
                        // http://stackoverflow.com/questions/13166284/eventsources-response-has-a-mime-type-text-html-that-is-not-text-event-str

                        h.Context.Response.ContentType = "text/event-stream";
                        h.Context.Response.Cache.SetCacheability(HttpCacheability.NoCache);

                        //EventSource's response has a MIME type ("text/plain") that is not "text/event-stream". Aborting the connection.


                        // A potentially dangerous Request.QueryString value was detected from the client (e="<client value="15.12...").
                        //var _e_xml = h.Context.Request.RawUrl
                        //    .SkipUntilLastOrEmpty("?")
                        //    .SkipUntilOrEmpty("e=")
                        //    .TakeUntilIfAny("&");

                        //var _e_xml_decoded = HttpUtility.UrlDecode(_e_xml);

                        var cookie_session = h.Context.Request.Cookies["session"];

                        //var _e = XElement.Parse(cookie_session.Value);

                        var session = int.Parse(cookie_session.Value);

                        //var c = new __ConsoleToDatabaseWriter(session);
                        var data = new SystemConsoleOut();

                        var header_id = h.Context.Request.Headers["Last-Event-ID"];



                        var id = 0;

                        if (header_id != null)
                        {
                            __ConsoleToDatabaseWriter.InternalWrite("Continue " + new { session, header_id }.ToString() + Environment.NewLine);

                            //id = int.Parse(XElement.Parse(header_id).Attribute("id").Value);
                            id = int.Parse(header_id);
                        }
                        else
                        {
                            data.SelectTransactionKey(session,
                                nextid =>
                                {
                                    id = (int)nextid;

                                    //var xml = new XElement("e",
                                    //    new XAttribute("id", id)
                                    //);
                                    __ConsoleToDatabaseWriter.InternalWrite("Reset To " + new { session, id }.ToString() + Environment.NewLine);

                                    h.Context.Response.Write("id: " + id + "\n\n");
                                    //h.Context.Response.Write("event: SystemConsoleOut\n");
                                    h.Context.Response.Write("data: reset to " + new { id } + " \n\n");
                                    h.Context.Response.Flush();

                                }
                            );
                        }

                        int retry = 1000 / 30;

                        Action CheckForUpdates = delegate
                        {
                            Action later = delegate { };
                            data.SelectTransactionKey(session,
                                  nextid =>
                                  {
                                      if (id == (int)nextid)
                                      {
                                          later = delegate
                                          {
                                              // no updates yet
                                              // it is imortant to sleep
                                              // only if we released the database.
                                              // otherwise other threads will wait for us!
                                              Thread.Sleep(retry);
                                          };

                                          return;
                                      }


                                      data.SelectContentUpdates(
                                          new SystemConsoleOutQueries.SelectContentUpdates
                                          {
                                              id = id,
                                              nextid = (int)nextid,
                                              session = session
                                          },
                                          r =>
                                          {
                                              string value = r.value;

                                              h.Context.Response.Write("event: SystemConsoleOut\n");
                                              h.Context.Response.Write("data: " +
                                                  value.Replace("\r", "\\r").Replace("\n", "\\n") + "\n\n");
                                              h.Context.Response.Flush();
                                          }
                                      );


                                      id = (int)nextid;
                                      //var xml = new XElement("e",
                                      //    new XAttribute("id", id)
                                      //);

                                      //h.Context.Response.Write("id: " + xml.ToString() + "\n\n");
                                      h.Context.Response.Write("id: " + id + "\n\n");
                                      h.Context.Response.Flush();

                                  }
                              );

                            later();
                        };

                        // loop forever?
                        var t = new Stopwatch();

                        t.Start();

                        //for (int i = 0; i < 1024; i++)
                        while (t.ElapsedMilliseconds < 7000)
                        {
                            // http://stackoverflow.com/questions/9743253/how-to-detect-a-disconnection-when-using-server-sent-events-on-asp-net

                            //if (!h.Context.Response.IsClientConnected)
                            //{
                            //    __ConsoleToDatabaseWriter.InternalWrite("!IsClientConnected");
                            //    break;
                            //}
                            //__ConsoleToDatabaseWriter.InternalWrite("CheckForUpdates " + new { Thread.CurrentThread.ManagedThreadId, session, i, id }.ToString() + Environment.NewLine);
                            CheckForUpdates();
                        }



                        //Thread.Sleep(5000);

                        #region prevent error 2, required if we did not send any other events
                        h.Context.Response.Write("retry: " + retry + "\n\n");
                        h.Context.Response.Write("data: retry later\n\n");
                        h.Context.Response.Flush();
                        #endregion

                        h.CompleteRequest();
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }


                }

            __ConsoleToDatabaseWriter.InternalWrite(
             new { h.Context.Request.Path, Accepts, h.Context.Response.ContentType } + " done!" + Environment.NewLine
         );

        }
    }


    class __ConsoleToDatabaseWriter : TextWriter
    {
        public SystemConsoleOut data = new SystemConsoleOut();

        public Action<string> AtWrite;

        public override void Write(string value)
        {
            AtWrite(value);
        }

        public override void WriteLine(string value)
        {
            AtWrite(value + Environment.NewLine);

        }

        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }

        public __ConsoleToDatabaseWriter(int session)
        {
            var w = this;

            InitializeAndKeepOriginal(w);

            w.AtWrite =
                x =>
                {
                    this.data.InsertContent(
                           new SystemConsoleOutQueries.InsertContent
                           {
                               session = session,
                               value = x
                           },
                           id =>
                           {


                               InternalWrite(x);

                           }
                    );

                    // db!
                };

        }


        [Conditional("DEBUG")]
        public static void InternalWriteLine(string x)
        {
            InternalWrite(x + Environment.NewLine);
        }

        [Conditional("DEBUG")]
        public static void InternalWrite(string x)
        {
            var i = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            //o.Write(new { session, id, x });
            if (o == null)
                Console.Out.Write(x);
            else
                o.Write(x);

            Console.ForegroundColor = i;
        }

        static TextWriter o;

        private static TextWriter InitializeAndKeepOriginal(__ConsoleToDatabaseWriter w)
        {
            // Console is not really thread safe!
            if (o == null)
                o = Console.Out;

            Console.SetOut(w);
            return o;
        }
    }
}
