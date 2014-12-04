using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibNative.SystemHeaders.android
{
    // "X:\opensource\android-ndk-r10c\platforms\android-9\arch-arm\usr\include\android\input.h"

    [Script(IsNative = true, Header = "android/input.h", IsSystemHeader = true)]
    public static class input
    {
        // http://mobilepearls.com/labs/native-android-api/

        // X:\jsc.svn\examples\c\android\Test\TestNDK\TestNDK\xNativeActivity.cs

        // AInputEvent
        [Script(IsNative = true)]
        public class AInputEvent
        {
        }
    }

}
