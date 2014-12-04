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

namespace TestNDKLooper
{
    public class xNativeActivity : ScriptCoreLibAndroidNDK.IAssemblyReferenceToken
    {
        // <!-- This .apk has no Java code itself, so set hasCode to false. -->

        // tested on android 2.4 galaxy s1
        // I/xNativeActivity(17047): enter TestNDKLooper
        // I/xNativeActivity(17047): exit TestNDKLooper

        // cmd /K c:\util\android-sdk-windows\platform-tools\adb.exe logcat
        // X:\jsc.svn\examples\c\android\Test\TestNDKLooper\TestNDKLooper\bin\Debug\staging
        // "C:\util\android-sdk-windows\tools\android.bat" update project -p . -s --target android-8
        // X:\opensource\android-ndk-r10c\ndk-build.cmd
        // "C:\util\apache-ant-1.9.2\bin\ant.bat" debug
        // "C:\util\android-sdk-windows\platform-tools\adb.exe" install -r "X:\jsc.svn\examples\c\android\Test\TestNDKLooper\TestNDKLooper\bin\Debug\staging\bin\NativeActivity-debug.apk"

        // ? rm failed for -f, Read-only file system

        //I/ActivityManager(  482): Start proc com.example.TestNDKLooper for activity com.example.TestNDKLooper/android.app.NativeActivity: pid=26265 uid=10093 gids={50093}
        //I/dalvikvm(26265): Enabling JNI app bug workarounds for target SDK version 9...
        //V/PhoneStatusBar(  568): setLightsOn(true)
        //D/AndroidRuntime(26265): Shutting down VM
        //W/dalvikvm(26265): threadid=1: thread exiting with uncaught exception (group=0x41701ba8)
        //E/AndroidRuntime(26265): FATAL EXCEPTION: main
        //E/AndroidRuntime(26265): Process: com.example.TestNDKLooper, PID: 26265
        //E/AndroidRuntime(26265): java.lang.RuntimeException: Unable to start activity ComponentInfo{com.example.TestNDKLooper/android.app.NativeActivity}: java.lang.IllegalArgumentException: Unable to find native library: TestNDKLooper


        // "X:\opensource\android-ndk-r10c\samples\native-activity\jni\main.c"

        // X:\opensource\android-ndk-r10c\platforms\android-21\arch-arm\usr\include\android\log.h
        // int __android_log_print(int prio, const char *tag,  const char *fmt, ...)
        // #define LOGI(...) ((void)__android_log_print(ANDROID_LOG_INFO, "native-activity", __VA_ARGS__))

        [Script(NoDecoration = true)]
        static void android_main(android_native_app_glue.android_app state)
        {
            // http://supersegfault.com/three-ways-to-set-up-ndk-apps/

            // jsc is not printing the target name?
            var loc0 = state;

            // roslyn confuses jsc?

            android_native_app_glue.app_dummy();

            log.__android_log_print(log.android_LogPriority.ANDROID_LOG_INFO, "xNativeActivity", "enter TestNDKLooper");

            // state<T>.userData
            state.userData = default(object);
            //state.

            // http://stackoverflow.com/questions/18316046/when-build-with-latest-android-ndk-nativeactivity-spams-to-log-on-touch-events

            // can we do events in C just yet?
            #region onAppCmd
            state.onAppCmd = (app, cmd) =>
            {
                // http://supersegfault.com/three-ways-to-set-up-ndk-apps/
                // native callbacks wont like scope/instance pointers

                log.__android_log_print(0, "xNativeActivity", "onAppCmd");

                // enum tostring for c available yet?
                if (cmd == android_native_app_glue.android_app_cmd.APP_CMD_INIT_WINDOW)
                {
                    log.__android_log_print(0, "xNativeActivity", "APP_CMD_INIT_WINDOW");
                }

                if (cmd == android_native_app_glue.android_app_cmd.APP_CMD_GAINED_FOCUS)
                {
                    log.__android_log_print(0, "xNativeActivity", "APP_CMD_GAINED_FOCUS");
                }

                if (cmd == android_native_app_glue.android_app_cmd.APP_CMD_LOST_FOCUS)
                {
                    log.__android_log_print(0, "xNativeActivity", "APP_CMD_GAINED_FOCUS");
                }
            };
            #endregion

            //state.onInputEvent = delegate { };

            // http://supersegfault.com/three-ways-to-set-up-ndk-apps/

            var FD = 0;
            var events = 0;
            var source = default(android_native_app_glue.android_poll_source);

            var ok = 1 == 1;
            while (ok)
            {
                // num5 = ((int)ALooper_pollAll((int)0, ref_num1, ref_num2, ref_android_poll_source3));
                // num5 = ((int)ALooper_pollAll((int)0, &num1, &num2, &android_poll_source3));

                //jni/TestNDKLooper.dll.c: In function 'android_main':
                //jni/TestNDKLooper.dll.c:38:9: warning: passing argument 4 of 'ALooper_pollAll' from incompatible pointer type [enabled by default]
                //X:/opensource/android-ndk-r10c/platforms/android-9/arch-mips/usr/include/android/looper.h:194:5: note: expected 'void **' but argument is of type 'struct android_poll_source **'

                // Error	2	Argument 4: cannot convert from 'ref ScriptCoreLibNative.SystemHeaders.android_native_app_glue.android_poll_source' to 'ref object'	X:\jsc.svn\examples\c\android\Test\TestNDKLooper\TestNDKLooper\xNativeActivity.cs	112	25	TestNDKLooper

                // E/NativeActivity( 3001): channel '40bf2468 com.example.TestNDKLooper/android.app.NativeActivity (client)' ~ Failed to receive dispatch signal.  status=-11

                var ident = looper.ALooper_pollAll(
                    500,
                    ref FD,
                    ref events,
                    ref source
                );

                if (ident >= 0)
                {
                    log.__android_log_print(0, "xNativeActivity", "ALooper_pollAll gave source?");

                    if (source != null)
                    {
                        log.__android_log_print(0, "xNativeActivity", "ALooper_pollAll gave source? yes");

                        // (/* typecast */(void(*)(struct tag_struct android_app**, struct tag_struct android_poll_source**))android_poll_source3->process)((struct android_app*)state, (struct android_poll_source*)android_poll_source3);
                        source.process(state, source);
                    }
                }
            }

            log.__android_log_print(log.android_LogPriority.ANDROID_LOG_INFO, "xNativeActivity", "exit TestNDKLooper");



        }
    }
}
