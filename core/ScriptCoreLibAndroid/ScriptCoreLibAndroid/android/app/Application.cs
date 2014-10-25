using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using android.content;

namespace android.app
{
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/app/Application.java
    // http://developer.android.com/reference/android/app/Application.html

    [Script(IsNative = true)]
    public class Application : ContextWrapper
    {
        // members and types are to be extended by jsc at release build
    }
}
