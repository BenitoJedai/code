using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibNative.SystemHeaders.sys
{
    // "X:\opensource\android-ndk-r10c\platforms\android-12\arch-arm\usr\include\sys\socket.h"

    [Script(IsNative = true, Header = "sys/socket.h", IsSystemHeader = true)]
    public static class socket_h
    {
        public const int AF_INET = 2;
        public const int SOCK_DGRAM = 2;

        public const int IPPROTO_UDP = 17;
        public const int IPPROTO_IP = 0;

        public const int IP_MULTICAST_TTL = 33;
        public const int IP_MULTICAST_IF = 32;

        public const int SOL_SOCKET = 1;

        public const int SO_REUSEADDR = 2;

        // http://comments.gmane.org/gmane.comp.handhelds.android.ndk/22418
        // http://stackoverflow.com/questions/10408980/android-ndk-sockets-network-unreachable

        // what does it take to build 
        // a native app on the red server?
        // would it be useful for lan udb broadcasts?

        // http://www.roman10.net/simple-tcp-socket-client-and-server-communication-in-c-under-linux/



        // http://mobilepearls.com/labs/native-android-api/

        // X:\jsc.svn\examples\c\android\Test\TestNDK\TestNDK\xNativeActivity.cs



        // "X:\opensource\android-ndk-r10c\platforms\android-12\arch-arm\usr\include\sys\socketcalls.h"
        // #define SYS_SENDTO      11              /* sys_sendto(2)                */

        // Show Details	Severity	Code	Description	Project	File	Line
        //Error CS0542	'socket': member names cannot be the same as their enclosing type ScriptCoreLibAndroidNDK socket.cs	35

        public static int socket(int af, int sock, int ipproto) => default(int);


        public unsafe static int setsockopt(int s, int a, int b, void* buffer, int socklen_t) => default(int);


        // http://cyberkinetica.homeunix.net/os2tk45/tcppr/087_L3_Multicastingandthese.html
        [Script(IsNative = true)]
        public struct in_addr
        {
            ulong s_addr;
        }
    }

}
