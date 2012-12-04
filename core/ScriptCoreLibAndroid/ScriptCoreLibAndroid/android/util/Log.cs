﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using ScriptCoreLib;

namespace android.util
{
    // http://developer.android.com/reference/android/util/Log.html#wtf(java.lang.String, java.lang.String)
    // https://github.com/android/platform_frameworks_base/blob/master/core/java/android/util/Log.java
    [Script(IsNative = true)]
    public  class Log
    {
        public static string getStackTraceString(java.lang.Throwable t)
        {
            return default(string);
        }

        public static int i(string tag, string msg)
        {
            return default(int);
        }

        public static int wtf(string tag, string msg)
        {
            return default(int);
        }
    }
}
