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

    [Script(Implements = typeof(InteractiveObject))]
    internal static class __InteractiveObject
    {

        #region click
        public static void add_click(InteractiveObject that, Action<MouseEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MouseEvent.CLICK);
        }

        public static void remove_click(InteractiveObject that, Action<MouseEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MouseEvent.CLICK);
        }
        #endregion



        #region doubleClick
        public static void add_doubleClick(InteractiveObject that, Action<MouseEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MouseEvent.DOUBLE_CLICK);
        }

        public static void remove_doubleClick(InteractiveObject that, Action<MouseEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MouseEvent.DOUBLE_CLICK);
        }
        #endregion

        #region focusIn
        public static void add_focusIn(InteractiveObject that, Action<FocusEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, FocusEvent.FOCUS_IN);
        }

        public static void remove_focusIn(InteractiveObject that, Action<FocusEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, FocusEvent.FOCUS_IN);
        }
        #endregion

        #region focusOut
        public static void add_focusOut(InteractiveObject that, Action<FocusEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, FocusEvent.FOCUS_OUT);
        }

        public static void remove_focusOut(InteractiveObject that, Action<FocusEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, FocusEvent.FOCUS_OUT);
        }
        #endregion

        #region keyDown
        public static void add_keyDown(InteractiveObject that, Action<KeyboardEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, KeyboardEvent.KEY_DOWN);
        }

        public static void remove_keyDown(InteractiveObject that, Action<KeyboardEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, KeyboardEvent.KEY_DOWN);
        }
        #endregion

        #region keyFocusChange
        public static void add_keyFocusChange(InteractiveObject that, Action<FocusEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, FocusEvent.KEY_FOCUS_CHANGE);
        }

        public static void remove_keyFocusChange(InteractiveObject that, Action<FocusEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, FocusEvent.KEY_FOCUS_CHANGE);
        }
        #endregion

        #region keyUp
        public static void add_keyUp(InteractiveObject that, Action<KeyboardEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, KeyboardEvent.KEY_UP);
        }

        public static void remove_keyUp(InteractiveObject that, Action<KeyboardEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, KeyboardEvent.KEY_UP);
        }
        #endregion

        #region mouseDown
        public static void add_mouseDown(InteractiveObject that, Action<MouseEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MouseEvent.MOUSE_DOWN);
        }

        public static void remove_mouseDown(InteractiveObject that, Action<MouseEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MouseEvent.MOUSE_DOWN);
        }
        #endregion

        #region mouseFocusChange
        public static void add_mouseFocusChange(InteractiveObject that, Action<FocusEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, FocusEvent.MOUSE_FOCUS_CHANGE);
        }

        public static void remove_mouseFocusChange(InteractiveObject that, Action<FocusEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, FocusEvent.MOUSE_FOCUS_CHANGE);
        }
        #endregion

        #region mouseMove
        public static void add_mouseMove(InteractiveObject that, Action<MouseEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MouseEvent.MOUSE_MOVE);
        }

        public static void remove_mouseMove(InteractiveObject that, Action<MouseEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MouseEvent.MOUSE_MOVE);
        }
        #endregion

        #region mouseOut
        public static void add_mouseOut(InteractiveObject that, Action<MouseEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MouseEvent.MOUSE_OUT);
        }

        public static void remove_mouseOut(InteractiveObject that, Action<MouseEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MouseEvent.MOUSE_OUT);
        }
        #endregion

        #region mouseOver
        public static void add_mouseOver(InteractiveObject that, Action<MouseEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MouseEvent.MOUSE_OVER);
        }

        public static void remove_mouseOver(InteractiveObject that, Action<MouseEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MouseEvent.MOUSE_OVER);
        }
        #endregion

        #region mouseUp
        public static void add_mouseUp(InteractiveObject that, Action<MouseEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MouseEvent.MOUSE_UP);
        }

        public static void remove_mouseUp(InteractiveObject that, Action<MouseEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MouseEvent.MOUSE_UP);
        }
        #endregion

        #region mouseWheel
        public static void add_mouseWheel(InteractiveObject that, Action<MouseEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MouseEvent.MOUSE_WHEEL);
        }

        public static void remove_mouseWheel(InteractiveObject that, Action<MouseEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MouseEvent.MOUSE_WHEEL);
        }
        #endregion

        #region rollOut
        public static void add_rollOut(InteractiveObject that, Action<MouseEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MouseEvent.ROLL_OUT);
        }

        public static void remove_rollOut(InteractiveObject that, Action<MouseEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MouseEvent.ROLL_OUT);
        }
        #endregion

        #region rollOver
        public static void add_rollOver(InteractiveObject that, Action<MouseEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MouseEvent.ROLL_OVER);
        }

        public static void remove_rollOver(InteractiveObject that, Action<MouseEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MouseEvent.ROLL_OVER);
        }
        #endregion

        #region tabChildrenChange
        public static void add_tabChildrenChange(InteractiveObject that, Action<Event> value)
        {
            CommonExtensions.CombineDelegate(that, value, Event.TAB_CHILDREN_CHANGE);
        }

        public static void remove_tabChildrenChange(InteractiveObject that, Action<Event> value)
        {
            CommonExtensions.RemoveDelegate(that, value, Event.TAB_CHILDREN_CHANGE);
        }
        #endregion

        #region tabEnabledChange
        public static void add_tabEnabledChange(InteractiveObject that, Action<Event> value)
        {
            CommonExtensions.CombineDelegate(that, value, Event.TAB_ENABLED_CHANGE);
        }

        public static void remove_tabEnabledChange(InteractiveObject that, Action<Event> value)
        {
            CommonExtensions.RemoveDelegate(that, value, Event.TAB_ENABLED_CHANGE);
        }
        #endregion

        #region tabIndexChange
        public static void add_tabIndexChange(InteractiveObject that, Action<Event> value)
        {
            CommonExtensions.CombineDelegate(that, value, Event.TAB_INDEX_CHANGE);
        }

        public static void remove_tabIndexChange(InteractiveObject that, Action<Event> value)
        {
            CommonExtensions.RemoveDelegate(that, value, Event.TAB_INDEX_CHANGE);
        }
        #endregion





    }
}
