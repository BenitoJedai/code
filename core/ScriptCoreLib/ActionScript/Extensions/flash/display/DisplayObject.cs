using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.events;

namespace ScriptCoreLib.ActionScript.Extensions.flash.display
{
    // if a type implements a type that is set to be native, then only implementation
    // which is marked with NotImplementedHere applies

    [Script(Implements = typeof(DisplayObject))]
    internal static class __DisplayObject
    {
        #region added
        public static void add_added(DisplayObject that, Action<Event> value)
        {
            CommonExtensions.CombineDelegate(that, value, Event.ADDED);
        }

        public static void remove_added(DisplayObject that, Action<Event> value)
        {
            CommonExtensions.RemoveDelegate(that, value, Event.ADDED);
        }
        #endregion

        
        

    }
}
