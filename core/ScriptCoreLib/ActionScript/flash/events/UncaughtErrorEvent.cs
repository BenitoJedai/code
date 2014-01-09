using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.events
{
    // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/events/UncaughtErrorEvent.html
    [Script(IsNative = true)]
    public class UncaughtErrorEvent : ErrorEvent
    {
        public UncaughtErrorEvent(string type)
            : base(type)
        {

        }
        public object error { get; private set; }

        public static readonly string UNCAUGHT_ERROR = "uncaughtError";
    }
}