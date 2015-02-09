using ScriptCoreLib;
using ScriptCoreLibNative.SystemHeaders;
using ScriptCoreLibNative.SystemHeaders.android;
using ScriptCoreLibNative.SystemHeaders.sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLibNative.SystemHeaders.sys.socket_h;
using System.Reflection;

[assembly: Obfuscation(Feature = "script")]

namespace TestNDKUDP
{
    public class xNativeActivity : ScriptCoreLibAndroidNDK.IAssemblyReferenceToken
    {
        // Error: X:\jsc.svn\examples\c\android\Test\TestNDKUDP\TestNDKUDP\bin\Debug is not a valid project (AndroidManifest.xml not found).



        [Script(NoDecoration = true)]
        unsafe static void android_main(android_native_app_glue.android_app state)
        {
            android_native_app_glue.app_dummy();



            //Action<

            // X:\jsc.svn\examples\c\android\Test\TestNDKLooper\TestNDKLooper\xNativeActivity.cs
            log.__android_log_print(log.android_LogPriority.ANDROID_LOG_INFO, "xNativeActivity", "enter TestNDKUDP");

            // listen to sockets.



            // X:\jsc.svn\examples\javascript\chrome\apps\ChromeUDPNotification\ChromeUDPNotification\Application.cs


            // http://stackoverflow.com/questions/10408980/android-ndk-sockets-network-unreachable

            // http://stackoverflow.com/questions/1593946/what-is-af-inet-and-why-do-i-need-it
            // https://msdn.microsoft.com/en-us/library/windows/hardware/ff543744(v=vs.85).aspx



            // http://stackoverflow.com/questions/6033581/using-socket-in-android-ndk



            // can we load apk from udp? and reload on update?
            var s = socket(
                AF_INET,
                SOCK_DGRAM,
                IPPROTO_UDP
            );

            //log.__android_log_print(log.android_LogPriority.ANDROID_LOG_INFO, "xNativeActivity", "enter TestNDKUDP");
            if (s < 0)
            {
                // (s < 0) s: -1
                // C : Opcode not implemented: ldind.i4 at TestNDKUDP.xNativeActivity.android_main

                // I/xNativeActivity(13196): enter TestNDKUDP
                //I/xNativeActivity(13196): (s < 0) s: -1, errno: 13
                // http://stackoverflow.com/questions/23870808/oserror-errno-13-permission-denied

                // OSError - Errno 13 Permission denied

                var errno = *errno_h.__errno();


                log.__android_log_print(log.android_LogPriority.ANDROID_LOG_INFO,
                    "xNativeActivity",
                    "(s < 0) s: %i, errno: %i",
                    __arglist(s, errno)
                    );

                return;
            }

            log.__android_log_print(log.android_LogPriority.ANDROID_LOG_INFO, "xNativeActivity", ":59");

            var bAllowMultiple = true;



            setsockopt(s, SOL_SOCKET, SO_REUSEADDR, &bAllowMultiple, sizeof(bool));

            //Error CS0118  'hopLimit' is a variable but is used like a type TestNDKUDP  xNativeActivity.cs  67

            byte hopLimit = 2;
            log.__android_log_print(log.android_LogPriority.ANDROID_LOG_INFO, "xNativeActivity", ":70");
            setsockopt(s, IPPROTO_IP, IP_MULTICAST_TTL, &hopLimit, sizeof(byte));

            // http://www.tldp.org/HOWTO/Multicast-HOWTO-6.html
            // struct in_addr interface_addr;
            in_addr localAddr;

            log.__android_log_print(log.android_LogPriority.ANDROID_LOG_INFO, "xNativeActivity", ":77");
            setsockopt(s, IPPROTO_IP, IP_MULTICAST_IF, &localAddr, sizeof(in_addr));


            //after:
            log.__android_log_print(log.android_LogPriority.ANDROID_LOG_INFO, "xNativeActivity", ":79");


            //            I / xNativeActivity(13271): enter TestNDKUDP
            //I / xNativeActivity(13271): :59
            //I / xNativeActivity(13271): :70
            //I / xNativeActivity(13271): :77
            //I / xNativeActivity(13271): :79
        }

    }
}
