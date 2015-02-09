using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibNative.SystemHeaders.android
{
    // "X:\opensource\android-ndk-r10c\platforms\android-9\arch-arm\usr\include\android\log.h"

    [Script(IsNative = true, Header = "android/log.h", IsSystemHeader = true)]
    public static class log
    {
        // X:\jsc.svn\core\ScriptCoreLibAndroid\ScriptCoreLibAndroid\android\util\Log.cs

        // X:\opensource\android-ndk-r10c\platforms\android-21\arch-arm\usr\include\android\log.h
        // int __android_log_print(int prio, const char *tag,  const char *fmt, ...)
        // #define LOGI(...) ((void)__android_log_print(ANDROID_LOG_INFO, "native-activity", __VA_ARGS__))

        // X:\jsc.svn\examples\c\android\Test\TestNDK\TestNDK\xNativeActivity.cs

        // http://stackoverflow.com/questions/19703272/android-log-print-equivalent-for-printf
        public enum android_LogPriority
        {
            ANDROID_LOG_UNKNOWN = 0,
            ANDROID_LOG_DEFAULT,    /* only for SetMinPriority() */
            ANDROID_LOG_VERBOSE,
            ANDROID_LOG_DEBUG,
            ANDROID_LOG_INFO,
            ANDROID_LOG_WARN,
            ANDROID_LOG_ERROR,
            ANDROID_LOG_FATAL,
            ANDROID_LOG_SILENT,     /* only for SetMinPriority(); must be last */
        }

        public static int __android_log_print(android_LogPriority prio, string tag, string fmt) { return default(int); }

        // would be callable if it were a shared code project?
        public static int __android_log_print(android_LogPriority prio, string tag, string fmt, __arglist) { return default(int); }


    }

}
