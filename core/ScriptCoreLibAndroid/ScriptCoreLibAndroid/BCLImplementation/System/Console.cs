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
            android.util.Log.i("System.Console", e);
        }
    }
}
