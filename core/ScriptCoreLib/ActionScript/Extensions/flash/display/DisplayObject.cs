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

        #region addedToStage
        public static void add_addedToStage(DisplayObject that, Action<Event> value)
        {
            CommonExtensions.CombineDelegate(that, value, Event.ADDED_TO_STAGE);
        }

        public static void remove_addedToStage(DisplayObject that, Action<Event> value)
        {
            CommonExtensions.RemoveDelegate(that, value, Event.ADDED_TO_STAGE);
        }
        #endregion

        #region enterFrame
        public static void add_enterFrame(DisplayObject that, Action<Event> value)
        {
            CommonExtensions.CombineDelegate(that, value, Event.ENTER_FRAME);
        }

        public static void remove_enterFrame(DisplayObject that, Action<Event> value)
        {
            CommonExtensions.RemoveDelegate(that, value, Event.ENTER_FRAME);
        }
        #endregion

        #region removed
        public static void add_removed(DisplayObject that, Action<Event> value)
        {
            CommonExtensions.CombineDelegate(that, value, Event.REMOVED);
        }

        public static void remove_removed(DisplayObject that, Action<Event> value)
        {
            CommonExtensions.RemoveDelegate(that, value, Event.REMOVED);
        }
        #endregion

        #region removedFromStage
        public static void add_removedFromStage(DisplayObject that, Action<Event> value)
        {
            CommonExtensions.CombineDelegate(that, value, Event.REMOVED_FROM_STAGE);
        }

        public static void remove_removedFromStage(DisplayObject that, Action<Event> value)
        {
            CommonExtensions.RemoveDelegate(that, value, Event.REMOVED_FROM_STAGE);
        }
        #endregion

        #region render
        public static void add_render(DisplayObject that, Action<Event> value)
        {
            CommonExtensions.CombineDelegate(that, value, Event.RENDER);
        }

        public static void remove_render(DisplayObject that, Action<Event> value)
        {
            CommonExtensions.RemoveDelegate(that, value, Event.RENDER);
        }
        #endregion




        


    }
}
