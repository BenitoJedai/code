using ScriptCoreLib.ActionScript.flash.net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.events
{
    // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/events/ServerSocketConnectEvent.html
    [Script(IsNative = true)]
    public class ServerSocketConnectEvent : Event
    {
        public Socket socket { get; set; }

        public static readonly string CONNECT = "connect";

    }
}

