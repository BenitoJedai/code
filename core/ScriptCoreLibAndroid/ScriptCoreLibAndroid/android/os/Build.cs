using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.os
{
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/os/Build.java
    // http://developer.android.com/reference/android/os/Build.html
    [Script(IsNative = true)]
    public class Build
    {
        public static readonly string MODEL;
        public static readonly string PRODUCT;

        public static string getRadioVersion()
        {
            return default(string);
        }
    }

}
