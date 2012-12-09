using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLibJava.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.Console))]
    internal class __Console
    {
        // notice we are not using Android namespace!

        public static void WriteLine(object e)
        {
            WriteLine(e.ToString());
        }

        public static void WriteLine(string e)
        {
            // http://stackoverflow.com/questions/8888654/android-set-max-length-of-logcat-messages
            // So the real message size for both binary and non-binary logs is ~4076 bytes.

            while (true)
            {

                if (e.Length < 4000)
                {
                    android.util.Log.i("System.Console", e);
                    break;
                }

                var x = e.Substring(0, 4000);
                e = e.Substring(4000);

                android.util.Log.i("System.Console", x);
            }
        }
    }
}
