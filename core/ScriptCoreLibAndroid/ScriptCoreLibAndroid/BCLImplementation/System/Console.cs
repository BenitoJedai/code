using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Android.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.Console))]
    internal class __Console
    {
        public static void WriteLine(string e)
        {
            android.util.Log.wtf("System.Console", e);
        }
    }
}
