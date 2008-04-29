using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.ActionScript.flash.events;


namespace ScriptCoreLib.ActionScript.Extensions.flash.util
{
    [Script(Implements = typeof(Timer))]
    internal static class __Timer
    {

        #region timer
        public static void add_timer(Timer that, Action<TimerEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, TimerEvent.TIMER);
        }

        public static void remove_timer(Timer that, Action<TimerEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, TimerEvent.TIMER);
        }
        #endregion

    }
}
