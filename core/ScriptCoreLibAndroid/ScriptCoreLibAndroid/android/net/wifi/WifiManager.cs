using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.net.wifi
{
    // http://developer.android.com/reference/android/net/wifi/WifiManager.html
    [Script(IsNative = true)]
    public class WifiManager
    {
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
