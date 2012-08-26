using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.net
{
    // http://developer.android.com/reference/android/net/Uri.html
    [Script(IsNative = true)]
    public abstract class Uri
    {
        public static Uri parse(string value)
        {
            return default(Uri);
        }
    }
}
