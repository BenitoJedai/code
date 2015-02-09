using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibNative.SystemHeaders
{
    // "X:\opensource\android-ndk-r10c\platforms\android-12\arch-arm\usr\include\dlfcn.h"

    [Script(IsNative = true, Header = "dlfcn.h", IsSystemHeader = true)]
    public static class dlfcn
    {
        // http://mobilepearls.com/labs/native-android-api/
        // http://man7.org/linux/man-pages/man3/dlopen.3.html
      
        // can we pinvoke into a DRM .so ?
        // do we have to generate shadow .lib?
    }

}
