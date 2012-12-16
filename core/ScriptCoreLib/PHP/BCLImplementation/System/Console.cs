using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ScriptCoreLib.PHP.BCLImplementation.System
{

    [Script(Implements = typeof(global::System.Console))]
    internal class __Console
    {
        [Script]
        class __ConsoleTextWriter : TextWriter
        {
            public override void Write(string value)
            {
                InternalWrite(value);
            }

            public override void WriteLine(string value)
            {
                InternalOut.Write(value + Environment.NewLine);
            }

            public static void InternalWrite(string e)
            {
                Native.echo(e);
                Native.API.flush();
            }


            public override Encoding Encoding
            {
                get { return Encoding.UTF8; }
            }
        }

        static TextWriter InternalOut = new __ConsoleTextWriter();

        public static void SetOut(TextWriter newOut)
        {
            InternalOut = newOut;
        }

     
        public static void WriteLine(object e)
        {
            InternalOut.WriteLine(e.ToString());
        }

        public static void WriteLine(string e)
        {
            InternalOut.WriteLine(e);
        }

        public static void WriteLine()
        {
            InternalOut.WriteLine("");
        }

        //public static void WriteLine(string e, object x)
        //{
        //    __BrowserConsole.WriteLine(string.Format(e, x));
        //}

        public static void Write(string e)
        {
            InternalOut.Write(e);
        }

        public static void Write(object e)
        {
            InternalOut.Write(e.ToString());
        }
    }
}
