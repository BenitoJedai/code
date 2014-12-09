using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using ScriptCoreLib;

namespace android.util
{
    // http://developer.android.com/reference/android/util/Log.html#wtf(java.lang.String, java.lang.String)
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/util/Log.java

    [Script(IsNative = true)]
    public  class Log
    {
        // X:\jsc.svn\core\ScriptCoreLibAndroidNDK\ScriptCoreLibAndroidNDK\SystemHeaders\android\log.cs

        // X:\jsc.svn\examples\c\android\Test\TestNDK\TestNDK\xNativeActivity.cs
        // "X:\opensource\android-ndk-r10c\platforms\android-21\arch-arm\usr\include\android\log.h"

        public static string getStackTraceString(java.lang.Throwable t)
        {
            return default(string);
        }

        public static int e(string tag, int msg)
        {
            return default(int);
        }


        public static int e(string tag, string msg)
        {
            return default(int);
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
