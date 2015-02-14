using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibNative.SystemHeaders.sys
{
    // "X:\opensource\android-ndk-r10c\platforms\android-12\arch-arm\usr\include\sys\socket.h"

    public enum SOCKET { }

    [Script(IsNative = true, Header = "sys/socket.h", IsSystemHeader = true)]
    public static class socket_h
    {
        public const int INADDR_ANY = 0;

        public const int AF_INET = 2;
        public const int SOCK_DGRAM = 2;

        public const int IPPROTO_UDP = 17;
        public const int IPPROTO_IP = 0;

        public const int IP_MULTICAST_TTL = 33;
        public const int IP_MULTICAST_IF = 32;
        public const int IP_ADD_MEMBERSHIP = 35;

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms740532(v=vs.85).aspx
        public const int SOL_SOCKET = 1;

        public const int SO_REUSEADDR = 2;

        // http://comments.gmane.org/gmane.comp.handhelds.android.ndk/22418
        // http://stackoverflow.com/questions/10408980/android-ndk-sockets-network-unreachable

        // what does it take to build 
        // a native app on the red server?
        // would it be useful for lan udb broadcasts?

        // http://www.roman10.net/simple-tcp-socket-client-and-server-communication-in-c-under-linux/
        // http://www.sockets.com/ch16.htm



        // http://mobilepearls.com/labs/native-android-api/

        // X:\jsc.svn\examples\c\android\Test\TestNDK\TestNDK\xNativeActivity.cs
        // X:\jsc.svn\examples\c\android\Test\TestNDKUDP\TestNDKUDP\xNativeActivity.cs


        // "X:\opensource\android-ndk-r10c\platforms\android-12\arch-arm\usr\include\sys\socketcalls.h"
        // #define SYS_SENDTO      11              /* sys_sendto(2)                */

        // Show Details	Severity	Code	Description	Project	File	Line
        //Error CS0542	'socket': member names cannot be the same as their enclosing type ScriptCoreLibAndroidNDK socket.cs	35

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms740506(v=vs.85).aspx
        public static SOCKET socket(int af, int sock, int ipproto) => default(int);


        public unsafe static int setsockopt(this SOCKET s, int level, int optname, void* optval, int optlen) => default(int);
        //int setsockopt(int s, int level, int optname, const void* optval, int optlen);

        public unsafe static int bind(this SOCKET s, sockaddr* name, int namelen) => default(int);
        //__socketcall int bind(int, const struct sockaddr *, int);

        // http://cyberkinetica.homeunix.net/os2tk45/tcppr/087_L3_Multicastingandthese.html
        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms738571(v=vs.85).aspx
        [Script(IsNative = true)]
        public struct in_addr
        {
            public ulong s_addr;

            // 8
        }



        public static ulong inet_addr(this string cp)  => default(long);


        [Script(IsNative = true)]
        public struct ip_mreq
        {
            public in_addr imr_multiaddr;
            public in_addr imr_interface;
            // 16
        }

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms740496(v=vs.85).aspx
        // http://stackoverflow.com/questions/18609397/whats-the-difference-between-sockaddr-and-sockaddr-insockaddr-in6
        [Script(IsNative = true)]
        public unsafe struct sockaddr
        {
            ushort sa_family;
            // 4

            // buffer
            fixed byte sa_data[14];

            // 18
        };


        // X:\opensource\android-ndk-r10c\platforms\android-12\arch-arm\usr\include\linux\in.h
        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms740496(v=vs.85).aspx
        // http://stackoverflow.com/questions/2310103/why-a-c-sharp-struct-cannot-be-inherited
        [Script(IsNative = true)]
        public unsafe struct sockaddr_in // : sockaddr
        {
            public short sin_family;

            // http://stackoverflow.com/questions/19207745/htons-function-in-socket-programing
            // 4
            public ushort sin_port;
            // 8

            public  in_addr sin_addr;
            // 16

            public  fixed byte sin_zero[8];
            // 24
        };

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms738520(v=vs.85).aspx
        // uhp?
    }

}
