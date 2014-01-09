using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.events
{
    // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/events/UncaughtErrorEvents.html
    [Script(IsNative = true)]
    public class UncaughtErrorEvents : EventDispatcher
    {
        [method: Script(NotImplementedHere = true)]
        public event Action<UncaughtErrorEvent> uncaughtError;



    }
}

namespace ScriptCoreLib.ActionScript.Extensions.flash.display
{
    // if a type implements a type that is set to be native, then only implementation
    // which is marked with NotImplementedHere applies
    [Script(Implements = typeof(UncaughtErrorEvents))]
    internal static class __UncaughtErrorEvents
    {
        #region render
        public static void add_uncaughtError(UncaughtErrorEvents that, Action<UncaughtErrorEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, UncaughtErrorEvent.UNCAUGHT_ERROR);
        }

        public static void remove_uncaughtError(UncaughtErrorEvents that, Action<UncaughtErrorEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, UncaughtErrorEvent.UNCAUGHT_ERROR);
        }
        #endregion
    }
}