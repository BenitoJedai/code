using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.net.wifi
{
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/services/java/com/android/server/wifi/WifiService.java

    // https://android.googlesource.com/platform/frameworks/base.git/+/master/wifi/java/android/net/wifi/WifiManager.java

    // http://developer.android.com/reference/android/net/wifi/WifiManager.html
    [Script(IsNative = true)]
    public class WifiManager
    {
        // tested by?
        // X:\jsc.svn\examples\java\android\AndroidServiceUDPNotification\AndroidServiceUDPNotification\ApplicationActivity.cs
        // X:\jsc.svn\examples\java\android\forms\InteractivePortForwarding\InteractivePortForwarding\ApplicationActivity.cs

        public static int WIFI_MODE_FULL_HIGH_PERF = 0x00000003;

        public WifiManager.WifiLock createWifiLock(int lockType, string tag)
        {
            throw null;
        }

        public class WifiLock
        {
            public void acquire() { }
        }


        public MulticastLock createMulticastLock(string tag)
        {
            throw null;
        }

        // http://developer.android.com/reference/android/net/wifi/WifiManager.MulticastLock.html
        public class MulticastLock
        {
            public void acquire() { }
        }
    }
}
