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


    }
}
