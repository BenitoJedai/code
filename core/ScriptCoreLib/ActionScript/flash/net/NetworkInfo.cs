using ScriptCoreLib.ActionScript.flash.events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.net
{
    // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/net/NetworkInfo.html
    [Script(IsNative = true)]
    public class NetworkInfo : EventDispatcher
    {
        public static bool isSupported { get; private set; }



        public static NetworkInfo networkInfo { get; private set; }

        public Vector<NetworkInterface> findInterfaces()
        {
            return null;
        }
    }
}
