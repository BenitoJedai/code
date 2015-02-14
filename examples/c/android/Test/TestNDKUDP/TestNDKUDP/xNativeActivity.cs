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
using System.Runtime.CompilerServices;

[assembly: Obfuscation(Feature = "script")]

namespace TestNDKUDP
{
    public class xNativeActivity : ScriptCoreLibAndroidNDK.IAssemblyReferenceToken
    {
        // double click on exe./metro?
        // and have it run on android?

        // Error: X:\jsc.svn\examples\c\android\Test\TestNDKUDP\TestNDKUDP\bin\Debug is not a valid project (AndroidManifest.xml not found).


        // https://msdn.microsoft.com/en-us/library/hh534540.aspx
        static void trace(
            string message = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
            => log.__android_log_print(
                log.android_LogPriority.ANDROID_LOG_INFO,
                "xNativeActivity",
                //"line %i file %s",
                "%s:%i %s",
                __arglist(
                    sourceFilePath,
                    sourceLineNumber,
                    message
                )
            );

        // http://stackoverflow.com/questions/24581245/send-broadcast-from-c-code


        [Script(NoDecoration = true)]
        unsafe static void android_main(android_native_app_glue.android_app state)
        {
            android_native_app_glue.app_dummy();
            //Action<

            trace("enter TestNDKUDP");

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

            #region errno
            if (s < 0)
            {
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
            #endregion


            trace();

            var bAllowMultiple = true;
            s.setsockopt(SOL_SOCKET, SO_REUSEADDR, &bAllowMultiple, sizeof(bool));
            trace();

            //byte hopLimit = 2;
            //  The default value is one for all IP multicast datagrams. 
            //setsockopt(s, IPPROTO_IP, IP_MULTICAST_TTL, &hopLimit, sizeof(byte));

            // http://www.tldp.org/HOWTO/Multicast-HOWTO-6.html
            // struct in_addr interface_addr;
            in_addr localAddr;

            // For multicast sending use an IP_MULTICAST_IF flag with the setsockopt() call. This specifies the interface to be used.
            s.setsockopt(IPPROTO_IP, IP_MULTICAST_IF, &localAddr, sizeof(in_addr));
            trace();
            
            // http://www.phonesdevelopers.com/1817807/


            ip_mreq mreq;

            // "239.1.2.3"
            mreq.imr_multiaddr.s_addr = "239.1.2.3".inet_addr();

            s.setsockopt(IPPROTO_IP, IP_ADD_MEMBERSHIP, &mreq, sizeof(ip_mreq));

            trace();

            // Create the local endpoint
            sockaddr_in localEndPoint;

            localEndPoint.sin_family = AF_INET;
            localEndPoint.sin_addr.s_addr = INADDR_ANY;
            localEndPoint.sin_port = (ushort)0x5555;


            // Bind the socket to the port
            int r = s.bind((sockaddr*)&localEndPoint, sizeof(sockaddr_in));

            trace();



            //            I / xNativeActivity(13271): enter TestNDKUDP
            //I / xNativeActivity(13271): :59
            //I / xNativeActivity(13271): :70
            //I / xNativeActivity(13271): :77
            //I / xNativeActivity(13271): :79

            // http://pubs.opengroup.org/onlinepubs/7908799/xns/arpainet.h.html
            // http://stackoverflow.com/questions/15569012/android-udp-client-not-able-to-receive-data-on-non-rooted-phone
            // http://www.phonesdevelopers.com/1817807/

            // !!1
            // https://www.mail-archive.com/android-developers@googlegroups.com/msg115225.html

            // http://www.gta.ufrj.br/ensino/eel878/sockets/inet_ntoaman.html

            // could jsc web apps be turned into ndk servers?
        }


        //        jni/TestNDKUDP.dll.c: In function 'android_main':
        //jni/TestNDKUDP.dll.c:102:17: error: request for member 'sin_family' in something not a structure or union
        //     sockaddr_in4.sin_family = 2;
        //                 ^
        //jni/TestNDKUDP.dll.c:104:17: error: request for member 'sin_port' in something not a structure or union
        //     sockaddr_in4.sin_port = 21845;
        //                 ^
        //jni/TestNDKUDP.dll.c:105:18: warning: passing argument 2 of 'bind' from incompatible pointer type
        //     num5 = ((int)bind((int)t0, (struct sockaddr**)&sockaddr_in4, (int)sizeof(struct sockaddr_in*)));
        //                  ^
        //In file included from jni/TestNDKUDP.dll.h:22:0,
        //                 from jni/TestNDKUDP.dll.c:2:
        //X:/opensource/android-ndk-r10c/platforms/android-21/arch-arm64/usr/include/sys/socket.h:273:18: note: expected 'const struct sockaddr *' but argument is of type 'struct sockaddr **'
        // __socketcall int bind(int, const struct sockaddr*, int);
        //                  ^
        //make.exe: *** [obj/local/arm64-v8a/objs/TestNDKUDP/TestNDKUDP.dll.o]
        //        Error 1
    }
}
