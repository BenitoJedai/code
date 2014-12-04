using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibNative.SystemHeaders.android
{
    // https://gist.github.com/fornwall/553308#file-looper-h
    // "X:\opensource\android-ndk-r10c\platforms\android-9\arch-arm\usr\include\android\looper.h"

    [Script(IsNative = true, Header = "android/looper.h", IsSystemHeader = true)]
    public static class looper
    {
        // http://mobilepearls.com/labs/native-android-api/

        // X:\jsc.svn\examples\c\android\Test\TestNDK\TestNDK\xNativeActivity.cs

        // ALooper

        // ALooper_pollAll

        /**
 * Like ALooper_pollOnce(), but performs all pending callbacks until all
 * data has been consumed or a file descriptor is available with no callback.
 * This function will never return ALOOPER_POLL_CALLBACK.
 */
        //public static int ALooper_pollAll(int timeoutMillis, int* outFd, int* outEvents, void** outData) { }



        //jni/TestNDKLooper.dll.c: In function 'android_main':
        //jni/TestNDKLooper.dll.c:38:9: warning: passing argument 4 of 'ALooper_pollAll' from incompatible pointer type [enabled by default]
        //X:/opensource/android-ndk-r10c/platforms/android-9/arch-mips/usr/include/android/looper.h:194:5: note: expected 'void **' but argument is of type 'struct android_poll_source **'


        // android_poll_source
        //public static int ALooper_pollAll(int timeoutMillis, ref int outFd, ref int outEvents, ref android_native_app_glue.android_poll_source outData)


        // Error	2	Argument 4: cannot convert from 'ref ScriptCoreLibNative.SystemHeaders.android_native_app_glue.android_poll_source' to 'ref object'	X:\jsc.svn\examples\c\android\Test\TestNDKLooper\TestNDKLooper\xNativeActivity.cs	112	25	TestNDKLooper

        //public static int ALooper_pollAll(int timeoutMillis, ref int outFd, ref int outEvents, ref object outData)

        // jsc now knows to cast to void** even if user code expects a type ref
        public static int ALooper_pollAll<TData>(int timeoutMillis, ref int outFd, ref int outEvents, ref TData outData)
        {
            return default(int);
        }
    }

}
