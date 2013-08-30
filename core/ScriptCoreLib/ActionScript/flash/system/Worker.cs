using ScriptCoreLib.ActionScript.flash.events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.system
{
    // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/system/Worker.html
    [Script(IsNative = true)]
    public class Worker : EventDispatcher
    {
        // Each additional worker is created from a separate swf. 
    }
}
