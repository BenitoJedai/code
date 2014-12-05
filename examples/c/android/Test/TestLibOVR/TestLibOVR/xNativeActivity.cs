using ScriptCoreLibNative.SystemHeaders.android;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib;
using ScriptCoreLibNative.SystemHeaders;

[assembly: Obfuscation(Feature = "script")]

namespace TestLibOVR
{
    public class xNativeActivity : ScriptCoreLibAndroidNDK.IAssemblyReferenceToken
    {
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20141205
        // "X:\jsc.svn\examples\c\android\Test\TestLibOVR\TestLibOVR\bin\Debug\staging\libs\armeabi-v7a\libOculusPlugin.so"

        //        jni/Android.mk:32: cflags.mk: No such file or directory
        //make.exe: *** No rule to make target `cflags.mk'.  Stop.

        //ndk-build
        //Android NDK: WARNING: APP_PLATFORM android-19 is larger than android:minSdkVersion 9 in ./AndroidManifest.xml

        //Android NDK: ERROR:jni/Android.mk:jpeg: LOCAL_SRC_FILES points to a missing file
        //Android NDK: Check that jni/../../VRLib/jni/3rdParty/libjpeg.a exists  or that its path is correct
        //X:/opensource/android-ndk-r10c/build/core/prebuilt-library.mk:45: *** Android NDK: Aborting    .  Stop.


        // <!-- This .apk has no Java code itself, so set hasCode to false. -->

        // tested on android 2.4 galaxy s1
        // I/xNativeActivity(17047): enter TestLibOVR
        // I/xNativeActivity(17047): exit TestLibOVR

        // cmd /K c:\util\android-sdk-windows\platform-tools\adb.exe logcat
        // X:\jsc.svn\examples\c\android\Test\TestLibOVR\TestLibOVR\bin\Debug\staging
        // "C:\util\android-sdk-windows\tools\android.bat" update project -p . -s --target android-8
        // X:\opensource\android-ndk-r10c\ndk-build.cmd
        // "C:\util\apache-ant-1.9.2\bin\ant.bat" debug
        // "C:\util\android-sdk-windows\platform-tools\adb.exe" install -r "X:\jsc.svn\examples\c\android\Test\TestLibOVR\TestLibOVR\bin\Debug\staging\bin\NativeActivity-debug.apk"

        // ? rm failed for -f, Read-only file system

        //I/ActivityManager(  482): Start proc com.example.TestLibOVR for activity com.example.TestLibOVR/android.app.NativeActivity: pid=26265 uid=10093 gids={50093}
        //I/dalvikvm(26265): Enabling JNI app bug workarounds for target SDK version 9...
        //V/PhoneStatusBar(  568): setLightsOn(true)
        //D/AndroidRuntime(26265): Shutting down VM
        //W/dalvikvm(26265): threadid=1: thread exiting with uncaught exception (group=0x41701ba8)
        //E/AndroidRuntime(26265): FATAL EXCEPTION: main
        //E/AndroidRuntime(26265): Process: com.example.TestLibOVR, PID: 26265
        //E/AndroidRuntime(26265): java.lang.RuntimeException: Unable to start activity ComponentInfo{com.example.TestLibOVR/android.app.NativeActivity}: java.lang.IllegalArgumentException: Unable to find native library: TestLibOVR


        // "X:\opensource\android-ndk-r10c\samples\native-activity\jni\main.c"

        // X:\opensource\android-ndk-r10c\platforms\android-21\arch-arm\usr\include\android\log.h
        // int __android_log_print(int prio, const char *tag,  const char *fmt, ...)
        // #define LOGI(...) ((void)__android_log_print(ANDROID_LOG_INFO, "native-activity", __VA_ARGS__))

        [Script(NoDecoration = true)]
        static void android_main(android_native_app_glue.android_app state)
        {
            // http://supersegfault.com/three-ways-to-set-up-ndk-apps/

            // jsc is not printing the target name?
            //var loc0 = state;

            // roslyn confuses jsc?

            android_native_app_glue.app_dummy();

            log.__android_log_print(log.android_LogPriority.ANDROID_LOG_INFO, "xNativeActivity", "enter TestLibOVR");




        }
    }
}
