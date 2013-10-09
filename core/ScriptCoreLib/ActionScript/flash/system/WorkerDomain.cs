using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.system
{
    // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/system/WorkerDomain.html
    [Script(IsNative = true)]
    public class WorkerDomain : EventDispatcher
    {
        // Concurrency is not supported on mobile AIR platforms.

        // http://stackoverflow.com/questions/17669034/getting-a-swfs-bytes-from-within-the-swf
        // http://help.adobe.com/en_US/as3/dev/WS2f73111e7a180bd0-5856a8af1390d64d08c-7fff.html
        // http://help.adobe.com/en_US/as3/dev/WS2f73111e7a180bd0-5856a8af1390d64d08c-7fff.html
        // http://esdot.ca/site/2012/intro-to-as3-workers-part-3-nape-physics-starling
        // http://flexmonkey.blogspot.co.uk/2012/09/multi-threaded-sound-synthesis-in-flex.html

        //28d0:02:01 RewriteToAssembly error: System.InvalidOperationException: ScriptCoreLib.ActionScript.flash.system.WorkerDomain has conflicting members: isSupported
        public static bool isSupported { get; set; }

        public static Worker createWorker(ByteArray swf, bool giveAppPrivileges = false)
        {
            return null;
        }

    }
}
