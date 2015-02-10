using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibNative.SystemHeaders.sys
{
    // "X:\opensource\android-ndk-r10c\platforms\android-12\arch-arm\usr\include\linux\in.h"

    [Script(IsNative = true, Header = "linux/in.h", IsSystemHeader = true)]
    public static class in_h
    {
        

        // http://cyberkinetica.homeunix.net/os2tk45/tcppr/087_L3_Multicastingandthese.html
        [Script(IsNative = true)]
        public struct in_addr
        {
            ulong s_addr;
        }



        [Script(IsNative = true)]
        public struct ip_mreq
        {
            public in_addr imr_multiaddr;   /* IP multicast address of group */
            public in_addr imr_interface;   /* local IP address of interface */
        };
    }

}
