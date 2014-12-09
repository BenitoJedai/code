using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using ScriptCoreLib;
using java.util;

namespace android.os
{
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/os/BaseBundle.java

    // http://developer.android.com/reference/android/os/BaseBundle.html
    [Script(IsNative = true)]
    public class BaseBundle
    {
        // members and types are to be extended by jsc at release build

        public virtual object get(string key)
        {
            return null;
        }
    }
}
