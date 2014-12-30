using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using android.content;

namespace android.app
{
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/app/NativeActivity.java
    // https://developer.android.com/reference/android/app/NativeActivity.html

    /// <summary>
    /// Typical good candidates for the NDK are CPU-intensive workloads such as game engines, 
    /// signal processing, physics simulation, and so on. When examining whether or not you 
    /// should develop in native code, think about your requirements and see if the Android 
    /// framework APIs provide the functionality that you need.
    /// </summary>
    [Script(IsNative = true)]
    public class NativeActivity : Activity
    {
        // NASA's Johnson Space Center (JSC) in Houston, Texas, is home to the agency's Virtual Reality Lab
        // http://gizmodo.com/the-nasa-playground-that-takes-virtual-reality-to-a-who-1658637427


        // X:\jsc.svn\core\ScriptCoreLibAndroidNDK\ScriptCoreLibAndroidNDK\SystemHeaders\android\native_activity.cs

        // https://sourcex.wordpress.com/2013/11/12/art-runtime-for-kitkat-4-4-explained/
        // http://apcmag.com/android-runtime-how-it-works.htm

        // "X:\opensource\android-ndk-r10c\sources\android\native_app_glue\android_native_app_glue.h"
        // "X:\opensource\android-ndk-r10c\sources\android\native_app_glue\android_native_app_glue.c"
        // extern void android_main(struct android_app* app);

        // would native activity decrypt it secondary frame via dexclassloader?

        // X:\opensource\android-ndk-r10c\samples\native-activity
        // "C:\util\android-sdk-windows\tools\android.bat" update project -p . -s --target android-8
        // "C:\util\android-sdk-windows\tools\android.bat"
        //Error: The project either has no target set or the target is invalid.
        //Please provide a --target to the 'android.bat update' command.
        // -t --target     : Target ID to set for the project.
        // "C:\util\apache-ant-1.9.2\bin\ant.bat" debug
        // "X:\opensource\android-ndk-r10c\samples\native-activity\bin\NativeActivity-debug.apk"
        // "C:\util\android-sdk-windows\platform-tools\adb.exe" install -r "X:\jsc.svn\examples\c\android\Test\TestNDK\TestNDK\bin\Debug\staging\bin\NativeActivity-debug.apk"

        // http://www.adobe.com/devnet/air/articles/ane-android-devices.html
        // http://visualgdb.com/tutorials/android/native-activity/
        // X:\opensource\android-ndk-r10c\samples\native-activity\jni>X:\opensource\android-ndk-r10c\ndk-build.cmd
        // "X:\opensource\android-ndk-r10c\samples\native-activity\AndroidManifest.xml"

        // http://saidsecurity.wordpress.com/2012/10/11/how-to-compile-android-native-code-with-regular-makefile/
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201411/20141130

        // https://developer.android.com/tools/sdk/ndk/index.html
        // The jni directory can't change name. and run ndk-build in project_root directory.
        // Build your native code by running the 'ndk-build' script from your project's directory. 
        // X:\opensource\android-ndk-r10c\samples\hello-jni\jni

        // "X:\opensource\android-ndk-r10c\sources\android\native_app_glue\android_native_app_glue.c"
        // "X:\opensource\android-ndk-r10c\platforms\android-21\arch-arm\usr\include\android\native_activity.h"
        // "X:\opensource\android-ndk-r10c\sources\android\ndk_helper\JNIHelper.h"

        // members and types are to be extended by jsc at release build
    }
}
