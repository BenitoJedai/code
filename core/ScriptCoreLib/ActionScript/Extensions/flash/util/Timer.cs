using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.ActionScript.flash.events;

namespace ScriptCoreLib.ActionScript.Extensions.flash.util
{
    [Script(Implements = typeof(Timer))]
    public static class __Timer
    {
        #region timer
        public static void add_timer(Timer _this, Action<TimerEvent> value)
        {
            _this.addEventListener(TimerEvent.TIMER, value.ToFunction(), false, 0, false);
        }

        public static void remove_timer(Timer _this, Action<TimerEvent> value)
        {
            _this.removeEventListener(TimerEvent.TIMER, value.ToFunction(), false);
        }
        #endregion
    }
}
