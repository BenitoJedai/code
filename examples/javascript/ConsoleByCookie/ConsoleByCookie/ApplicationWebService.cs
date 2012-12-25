using ConsoleByCookie.Schema;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ConsoleByCookie
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
        public void CheckServerForSession(string session, Action<string> y)
        {
            new __ConsoleToDatabaseWriter(int.Parse(session));

            var message = new { CheckServerForSession = new { session = int.Parse(session).ToString("x8") } };

            Console.WriteLine(message);

            // Send it back to the caller.
            y(message.ToString());
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

            var o = Console.Out;

            Console.SetOut(w);

            w.AtWrite =
                x =>
                {
#if DEBUG
                    var i = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    o.Write(x);
                    Console.ForegroundColor = i;
#endif
                    this.data.InsertContent(
                        new SystemConsoleOutQueries.InsertContent
                        {
                            session = session,
                            value = x
                        }
                    );

                    // db!
                };

        }
    }
}
