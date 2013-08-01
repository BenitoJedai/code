using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ScriptCoreLibJava.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.Console))]
    internal class __Console
    {
        public static ConsoleColor ForegroundColor { get; set; }

        // notice we are not using Android namespace!
        // why not?

        public static void WriteLine(object e)
        {
            Out.WriteLine(e.ToString());
        }

        //        Implementation not found for type import :
        //type: System.Console
        //method: Void WriteLine()
        //Did you forget to add the [Script] attribute?
        //Please double check the signature!


        public static void WriteLine()
        {
            Out.WriteLine();
        }

        public static void WriteLine(string e)
        {
            Out.WriteLine(e);
        }

        public static void Write(string e)
        {
            Out.Write(e);
        }

        [Script]
        public class __ConsoleOut : TextWriter
        {
            public override Encoding Encoding
            {
                get { return Encoding.UTF8; }
            }

            public override void Write(string value)
            {
                __Console.InternalWriteLine(value);
            }

            public override void WriteLine(string value)
            {
                __Console.InternalWriteLine(value);
            }
        }

        static TextWriter InternalOut;
        public static TextWriter Out
        {
            get
            {
                if (InternalOut == null)
                    InternalOut = new __ConsoleOut();

                return InternalOut;
            }
        }

        public static void SetOut(global::System.IO.TextWriter newOut)
        {
            InternalOut = newOut;
        }


        private static string InternalWriteLine(string e)
        {
            // http://stackoverflow.com/questions/8888654/android-set-max-length-of-logcat-messages
            // So the real message size for both binary and non-binary logs is ~4076 bytes.

            var loop = true;

            while (loop)
            {

                if (e.Length < 4000)
                {
                    android.util.Log.i("System.Console", e);

                    loop = false;
                }
                else
                {
                    var x = e.Substring(0, 4000);
                    e = e.Substring(4000);

                    android.util.Log.i("System.Console", x);
                }
            }

            return e;
        }
    }
}
