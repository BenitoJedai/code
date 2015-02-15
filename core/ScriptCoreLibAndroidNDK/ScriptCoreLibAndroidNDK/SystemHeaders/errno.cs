using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibNative.SystemHeaders
{
    //"X:\opensource\android-ndk-r10c\platforms\android-12\arch-arm\usr\include\sys\errno.h"

    [Script(IsNative = true, Header = "errno.h", IsSystemHeader = true)]
    public unsafe static class errno_h
    {
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Exception.cs


        public static string strerror(int errnum) => default(string);

        /* internal function returning the address of the thread-specific errno */
        public static int* __errno() => default(int*);



        /* a macro expanding to the errno l-value */
        //#define errno   (*__errno())
    }

}
