using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using ScriptCoreLib;
using android.widget;
using android.os;

namespace android.webkit
{
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/webkit/SslErrorHandler.java
    // http://developer.android.com/reference/android/webkit/SslErrorHandler.html

    [Script(IsNative = true)]
    public class SslErrorHandler : Handler
    {
        // tested by ?



        public virtual void proceed() { }
    }

}
