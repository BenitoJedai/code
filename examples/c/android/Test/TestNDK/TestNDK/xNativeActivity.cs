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

namespace TestNDK
{
    public class xNativeActivity : ScriptCoreLibAndroidNDK.IAssemblyReferenceToken
    {
        // <!-- This .apk has no Java code itself, so set hasCode to false. -->

        // tested on android 2.4 galaxy s1
        // I/xNativeActivity(17047): enter TestNDK
        // I/xNativeActivity(17047): exit TestNDK

        // cmd /K c:\util\android-sdk-windows\platform-tools\adb.exe logcat
        // X:\jsc.svn\examples\c\android\Test\TestNDK\TestNDK\bin\Debug\staging
        // "C:\util\android-sdk-windows\tools\android.bat" update project -p . -s --target android-8
        // X:\opensource\android-ndk-r10c\ndk-build.cmd
        // "C:\util\apache-ant-1.9.2\bin\ant.bat" debug
        // "C:\util\android-sdk-windows\platform-tools\adb.exe" install -r "X:\jsc.svn\examples\c\android\Test\TestNDK\TestNDK\bin\Debug\staging\bin\NativeActivity-debug.apk"

        // ? rm failed for -f, Read-only file system

        //I/ActivityManager(  482): Start proc com.example.TestNDK for activity com.example.TestNDK/android.app.NativeActivity: pid=26265 uid=10093 gids={50093}
        //I/dalvikvm(26265): Enabling JNI app bug workarounds for target SDK version 9...
        //V/PhoneStatusBar(  568): setLightsOn(true)
        //D/AndroidRuntime(26265): Shutting down VM
        //W/dalvikvm(26265): threadid=1: thread exiting with uncaught exception (group=0x41701ba8)
        //E/AndroidRuntime(26265): FATAL EXCEPTION: main
        //E/AndroidRuntime(26265): Process: com.example.TestNDK, PID: 26265
        //E/AndroidRuntime(26265): java.lang.RuntimeException: Unable to start activity ComponentInfo{com.example.TestNDK/android.app.NativeActivity}: java.lang.IllegalArgumentException: Unable to find native library: TestNDK


        // "X:\opensource\android-ndk-r10c\samples\native-activity\jni\main.c"

        // X:\opensource\android-ndk-r10c\platforms\android-21\arch-arm\usr\include\android\log.h
        // int __android_log_print(int prio, const char *tag,  const char *fmt, ...)
        // #define LOGI(...) ((void)__android_log_print(ANDROID_LOG_INFO, "native-activity", __VA_ARGS__))
        // void android_main(struct android_app* state) {

        // void TestNDK_xNativeActivity_android_main(void* state)
        // void android_main(void* state)
        [Script(NoDecoration = true)]
        //static void android_main(object state)
        // void android_main(struct android_app* state)
        static void android_main(android_native_app_glue.android_app state)
        {
            // jsc is not printing the target name?
            var loc0 = state;

            // roslyn confuses jsc?

            android_native_app_glue.app_dummy();

            log.__android_log_print(log.android_LogPriority.ANDROID_LOG_INFO, "xNativeActivity", "enter TestNDK");

            // state<T>.userData
            state.userData = default(object);
            //state.

            // can we do events in C just yet?

            // X:\jsc.svn\examples\c\Test\TestRoslynStaticDelegate\TestRoslynStaticDelegate\Class1.cs

            state.onAppCmd = (app, cmd) =>
            // nonroslyn:
            // void TestNDK_xNativeActivity__android_main_b__0(struct android_app* app, int cmd)
            // roslyn 453:
            // state->onAppCmd = TestNDK_xNativeActivity___c__DisplayClass0__android_main_b__1;

            // void TestNDK_xNativeActivity___c__DisplayClass0__android_main_b__1(LPTestNDK_xNativeActivity___c__DisplayClass0 __that, struct android_app* app, int cmd)
            // void TestNDK_xNativeActivity___c__DisplayClass0__android_main_b__1(LPTestNDK_xNativeActivity___c__DisplayClass0, struct android_app*, int);


            {
                // native callbacks wont like scope/instance pointers

                log.__android_log_print(0, "xNativeActivity", "onAppCmd");

                // enum tostring for c available yet?
                if (cmd == android_native_app_glue.android_app_cmd.APP_CMD_INIT_WINDOW)
                {
                    log.__android_log_print(0, "xNativeActivity", "APP_CMD_INIT_WINDOW");
                }
            };
            //state.onInputEvent = delegate { };


            // Error	1	No overload for method '__android_log_print' takes 3 arguments	X:\jsc.svn\examples\c\android\Test\TestNDK\TestNDK\xNativeActivity.cs	27	13	TestNDK

            log.__android_log_print(log.android_LogPriority.ANDROID_LOG_INFO, "xNativeActivity", "exit TestNDK");

            //I/xNativeActivity(26856): enter TestNDK
            //I/xNativeActivity(26856): exit TestNDK

            // jsc needs to not define android_main as it is already defined as extern 
            // jsc needs to define android_app state
            // then we can remove temp c files from staging


        }
    }
}
