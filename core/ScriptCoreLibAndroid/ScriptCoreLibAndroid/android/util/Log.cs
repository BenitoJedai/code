using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using ScriptCoreLib;

namespace android.util
{
    // http://developer.android.com/reference/android/util/Log.html#wtf(java.lang.String, java.lang.String)
    [Script(IsNative = true)]
    public  class Log
    {
        public static int wtf(string tag, string msg)
        {
            return default(int);
        }
    }
}
