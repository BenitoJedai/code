using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.events
{
    // http://livedocs.adobe.com/flex/3/langref/flash/events/TimerEvent.html
    [Script(IsNative = true)]
    public class TimerEvent : Event
    {
        #region Constants
        /// <summary>
        /// [static] Defines the value of the type property of a timer event object.
        /// </summary>
        public static readonly string TIMER = "timer";

        /// <summary>
        /// [static] Defines the value of the type property of a timerComplete event object.
        /// </summary>
        public static readonly string TIMER_COMPLETE = "timerComplete";

        #endregion


    }
}
