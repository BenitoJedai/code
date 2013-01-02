using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TestNamedParameters.Schema;

namespace TestNamedParameters
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
            // { Message = Could not load file or assembly 'System.Data.SQLite, Version=1.0.82.0, Culture=neutral, PublicKeyToken=db937bc2d44ff139'
            // or one of its dependencies. The located assembly's manifest definition does not match the assembly reference. 
            // (Exception from HRESULT: 0x80131040), 
            //Could not load file or assembly 'System.Data.SQLite, Version=1.0.83.0, Culture=neutral, PublicKeyToken=db937bc2d44ff139' or one of its dependencies. The located assembly's manifest definition does not match the assembly reference. (Exception from HRESULT: 0x80131040)

            //wtf?

            var x = new __ConsoleToDatabaseWriter(y);


            Console.WriteLine("before data");

            var data = new TheGridTable();

            data.Create();

            data.Insert(
                new TheGridTableQueries.Insert { ContentValue = "v", ContentComment = "c" }
                , id =>
                {
                    Console.WriteLine("just inserted " + new { id });
                }
            );

            data.SelectContent(
                new TheGridTableQueries.SelectContentByParent(),
                r =>
                {
                    long ContentKey = r.ContentKey;

                    Console.WriteLine("found " + new { ContentKey });
                }
            );

            Console.WriteLine("done!");
            x.Dispose();
        }

    }



    class __ConsoleToDatabaseWriter : TextWriter, IDisposable
    {
        protected override void Dispose(bool disposing)
        {
            Console.SetOut(o);

            // base calls broken for PHP?
            //base.Dispose(disposing);
        }

        public Action<string> AtWrite;

        public override void Write(string value)
        {
            Console.SetOut(o);
            AtWrite(value);
            Console.SetOut(this);
        }

        public override void WriteLine(string value)
        {
            var x = sw.ElapsedMilliseconds + "ms " + value;
            Console.SetOut(o);
            AtWrite(x + Environment.NewLine);
            Console.SetOut(this);
        }

        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }

        Stopwatch sw;

        public __ConsoleToDatabaseWriter(Action<string> xAtWrite)
        {
            sw = new Stopwatch();
            sw.Start();

            InitializeAndKeepOriginal(this);

            this.AtWrite += xAtWrite;
        }


        public static void InternalWriteLine(string x)
        {
            InternalWrite(x + Environment.NewLine);
        }

        public static void InternalWrite(string x)
        {
#if DEBUG
            var i = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            //o.Write(new { session, id, x });
            //#endif

            //Log.i("ConsoleByCookie", x);

            if (o == null)
                Console.Out.Write(x);
            else
                o.Write(x);
            //#if DEBUG

            Console.ForegroundColor = i;
#endif
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
