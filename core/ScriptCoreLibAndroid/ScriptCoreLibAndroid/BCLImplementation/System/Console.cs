using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ScriptCoreLibJava.BCLImplementation.System
//namespace ScriptCoreLib.Android.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/console.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System/Console.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Console.cs

    [Script(Implements = typeof(global::System.Console))]
    internal class __Console
    {
        // https://codelab.wordpress.com/2014/11/03/how-to-use-standard-output-streams-for-logging-in-android-apps/

        public static ConsoleColor ForegroundColor { get; set; }
        public static ConsoleColor BackgroundColor { get; set; }

        // notice we are not using Android namespace!
        // !!! ScriptCoreLib will be cached and while translated to java both 
        // caches itself for java and android.

        public static void WriteLine(object e)
        {
            Out.WriteLine(e.ToString());
        }

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


        #region SetOut
        [Script]
        public class __OutWriter : TextWriter
        {
            public override Encoding Encoding
            {
                get { return Encoding.UTF8; }
            }


            static string __WritePending = "";


            public override void Write(string value)
            {
                //InternalWriteLine(value);

                // capture console colors?
                __WritePending += value;
            }

            public override void WriteLine(string value)
            {
                InternalWriteLine(__WritePending + value);
                __WritePending = "";
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

        static TextWriter InternalOut;
        public static TextWriter Out
        {
            get
            {
                if (InternalOut == null)
                    InternalOut = new __OutWriter();

                return InternalOut;
            }
        }

        public static void SetOut(global::System.IO.TextWriter newOut)
        {
            InternalOut = newOut;
        }
        #endregion


    }
}
