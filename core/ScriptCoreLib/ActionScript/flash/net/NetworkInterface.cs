using ScriptCoreLib.ActionScript.flash.events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.net
{
    // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/net/NetworkInterface.html
    [Script(IsNative = true)]
    public class NetworkInterface
    {
        public bool active;
        public string name;
        public string displayName;
        public Vector<InterfaceAddress> addresses;
    }
}
