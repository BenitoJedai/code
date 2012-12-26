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
                InternalWrite(value + Environment.NewLine);
            }

            public static void InternalWrite(string e)
            {
                Native.echo(e);

                // do we need flush here?
                Native.API.flush();
            }


            public override Encoding Encoding
            {
                get { return Encoding.UTF8; }
            }
        }

        static TextWriter InternalOut;

        public static global::System.IO.TextWriter Out
        {
            get
            {
                if (InternalOut == null)
                    InternalOut = new __ConsoleTextWriter();

                return InternalOut;
            }
        }

        public static void SetOut(TextWriter newOut)
        {
            InternalOut = newOut;
        }


        public static void WriteLine(object e)
        {
            Out.WriteLine(e.ToString());
        }

        public static void WriteLine(string e)
        {
            Out.WriteLine(e);
        }

        public static void WriteLine()
        {
            Out.WriteLine("");
        }

        //public static void WriteLine(string e, object x)
        //{
        //    __BrowserConsole.WriteLine(string.Format(e, x));
        //}

        public static void Write(string e)
        {
            Out.Write(e);
        }

        public static void Write(object e)
        {
            Out.Write(e.ToString());
        }
    }
}
