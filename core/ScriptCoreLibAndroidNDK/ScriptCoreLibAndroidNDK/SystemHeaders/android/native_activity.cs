using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibNative.SystemHeaders.android
{
    // "X:\opensource\android-ndk-r10c\platforms\android-9\arch-arm\usr\include\android\native_activity.h"

    [Script(IsNative = true, Header = "android/native_activity.h", IsSystemHeader = true)]
    public static class native_activity
    {
        // The android.app.NativeActivity java class and the corresponding native struct ANativeActivity 
        // defined in <android/native_activity.h> frees applications from having to write java glue code, 
        // as well as providing native input event support not available otherwise.



        // http://mobilepearls.com/labs/native-android-api/

        // X:\jsc.svn\examples\c\android\Test\TestNDK\TestNDK\xNativeActivity.cs


        //ANativeActivity 
        //    ANativeActivityCallbacks 


        // static field we need to set or redefine?
        // extern ANativeActivity_createFunc ANativeActivity_onCreate;
    }

}
