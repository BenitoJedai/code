using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.text;

namespace ScriptCoreLib.ActionScript.Extensions.flash.text
{
    // if a type implements a type that is set to be native, then only implementation
    // which is marked with NotImplementedHere applies

    [Script(Implements = typeof(TextField))]
    public static class __TextField
    {
        #region change
        public static void add_change(TextField _this, Action<Event> value)
        {
            _this.addEventListener(Event.CHANGE, value.ToFunction(), false, 0, false);
        }

        public static void remove_change(TextField _this, Action<Event> value)
        {
            _this.removeEventListener(Event.CHANGE, value.ToFunction(), false);
        }
        #endregion
    }
}
