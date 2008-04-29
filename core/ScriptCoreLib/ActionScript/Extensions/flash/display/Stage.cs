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

    
    [Script(Implements = typeof(Stage))]
    internal static class __Stage
    {
        #region fullScreen
        public static void add_fullScreen(Stage that, Action<FullScreenEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, FullScreenEvent.FULL_SCREEN);
        }

        public static void remove_fullScreen(Stage that, Action<FullScreenEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, FullScreenEvent.FULL_SCREEN);
        }
        #endregion

        #region mouseLeave
        public static void add_mouseLeave(Stage that, Action<Event> value)
        {
            CommonExtensions.CombineDelegate(that, value, Event.MOUSE_LEAVE);
        }

        public static void remove_mouseLeave(Stage that, Action<Event> value)
        {
            CommonExtensions.RemoveDelegate(that, value, Event.MOUSE_LEAVE);
        }
        #endregion

        #region resize
        public static void add_resize(Stage that, Action<Event> value)
        {
            CommonExtensions.CombineDelegate(that, value, Event.RESIZE);
        }

        public static void remove_resize(Stage that, Action<Event> value)
        {
            CommonExtensions.RemoveDelegate(that, value, Event.RESIZE);
        }
        #endregion

        


    }
}
