using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace dalvik.system
{
    // http://developer.android.com/reference/dalvik/system/DexClassLoader.html
    // https://android.googlesource.com/platform/libcore-snapshot/+/ics-mr1/dalvik/src/main/java/dalvik/system/DexClassLoader.java



    [Script(IsNative = true)]
    public class DexClassLoader
    {
        // can we test it for ART?

        // https://code.google.com/p/android/issues/detail?id=67030&q=libart.so&colspec=ID%20Type%20Status%20Owner%20Summary%20Stars

        // http://www.symantec.com/connect/blogs/android-class-loading-hijacking

        //  apps might make use of such “feature” for legitimate reasons (e.g., a game loading additional levels at runtime),
        // http://blog.iseclab.org/2014/03/06/execute-this-looking-at-code-loading-techniques-in-android/

        // can a non running project update the webgl shader code for android?
        // http://stackoverflow.com/questions/23739261/does-android-art-support-runtime-dynamic-class-loading-just-like-dalvik
        // http://blog.iseclab.org/2012/06/04/andrubis-a-tool-for-analyzing-unknown-android-applications-2/
        // http://blog.iseclab.org/2010/10/14/news-from-the-anubis-admins/

    }
}
