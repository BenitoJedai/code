using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using ScriptCoreLib;
using java.util;

namespace android.os
{
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/os/Bundle.java

    // http://developer.android.com/reference/android/os/Bundle.html
    [Script(IsNative = true)]
    public class Bundle
    {
        // members and types are to be extended by jsc at release build


        public int getInt(string e)
        {
            throw null;
        }

        public bool containsKey(string e)
        {
            throw null;
        }

        public Set keySet()
        {
            throw null;
        }
    }
}
