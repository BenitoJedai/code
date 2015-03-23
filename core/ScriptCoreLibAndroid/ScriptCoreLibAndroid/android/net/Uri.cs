using java.io;
using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.net
{
    // https://github.com/android/platform_frameworks_base/blob/master/core/java/android/net/Uri.java
    // http://developer.android.com/reference/android/net/Uri.html

    [Script(IsNative = true)]
    public abstract class Uri
    {
        public static Uri parse(string value)
        {
            return default(Uri);
        }

        public static Uri fromFile(File value)
        {
            return default(Uri);
        }
    }
}
