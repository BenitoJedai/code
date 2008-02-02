using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.events;

namespace ScriptCoreLib.ActionScript.Extensions.flash.events
{
    [Script(Implements = typeof(EventDispatcher))]
    public static class __EventDispatcher
    {
        #region activate
        public static void add_activate(EventDispatcher that, Action<Event> value)
        {
            ScriptCoreLib.ActionScript.Extensions.CommonExtensions.CombineDelegate(that, value, Event.ACTIVATE);
        }

        public static void remove_activate(EventDispatcher that, Action<Event> value)
        {
            ScriptCoreLib.ActionScript.Extensions.CommonExtensions.RemoveDelegate(that, value, Event.ACTIVATE);
        }
        #endregion


        #region deactivate
        public static void add_deactivate(EventDispatcher that, Action<Event> value)
        {
            ScriptCoreLib.ActionScript.Extensions.CommonExtensions.CombineDelegate(that, value, Event.DEACTIVATE);
        }

        public static void remove_deactivate(EventDispatcher that, Action<Event> value)
        {
            ScriptCoreLib.ActionScript.Extensions.CommonExtensions.RemoveDelegate(that, value, Event.DEACTIVATE);
        }
        #endregion


    }
}
