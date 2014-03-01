using ScriptCoreLib.ActionScript.flash.events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.net
{
    // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/net/Socket.html
    [Script(IsNative = true)]
    public class Socket : EventDispatcher
    {
        public uint bytesAvailable { get; set; }

        public bool connected { get; set; }

    }
}

