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
    public static class __InteractiveObject
    {

        #region dblClick
        public static void add_dblClick(InteractiveObject that, Action<MouseEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, MouseEvent.DOUBLE_CLICK);
        }

        public static void remove_dblClick(InteractiveObject that, Action<MouseEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, MouseEvent.DOUBLE_CLICK);
        }
        #endregion


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




        #region mouseWheel
        public static void add_mouseWheel(InteractiveObject _this, Action<MouseEvent> value)
        {
            _this.addEventListener(MouseEvent.MOUSE_WHEEL, value.ToFunction(), false, 0, false);
        }

        public static void remove_mouseWheel(InteractiveObject _this, Action<MouseEvent> value)
        {
            _this.removeEventListener(MouseEvent.MOUSE_WHEEL, value.ToFunction(), false);
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





    }
}
