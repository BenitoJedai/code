using ConsoleByCookie.Schema;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

        //public void InternalHandler(WebServiceHandler h)
        //{
        //    var c = h.Context.Request.Cookies["session"];

        //    this.session = c.Value;
        //}
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

            var o = InitializeAndKeepOriginal(w);

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


#if DEBUG
                               var i = Console.ForegroundColor;
                               Console.ForegroundColor = ConsoleColor.Yellow;
                               //o.Write(new { session, id, x });
                               o.Write(x);
                               Console.ForegroundColor = i;
#endif

                           }
                    );

                    // db!
                };

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
