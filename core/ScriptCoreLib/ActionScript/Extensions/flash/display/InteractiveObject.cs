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
        public static void add_click(InteractiveObject _this, Action<MouseEvent> value)
        {
            _this.addEventListener(MouseEvent.CLICK, value.ToFunction(), false, 0, false);
        }

        public static void remove_click(InteractiveObject _this, Action<MouseEvent> value)
        {
            _this.removeEventListener(MouseEvent.CLICK, value.ToFunction(), false);
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
        public static void add_mouseOver(InteractiveObject _this, Action<MouseEvent> value)
        {
            _this.addEventListener(MouseEvent.MOUSE_OVER, value.ToFunction(), false, 0, false);
        }

        public static void remove_mouseOver(InteractiveObject _this, Action<MouseEvent> value)
        {
            _this.removeEventListener(MouseEvent.MOUSE_OVER, value.ToFunction(), false);
        }
        #endregion

        #region mouseOut
        public static void add_mouseOut(InteractiveObject _this, Action<MouseEvent> value)
        {
            _this.addEventListener(MouseEvent.MOUSE_OUT, value.ToFunction(), false, 0, false);
        }

        public static void remove_mouseOut(InteractiveObject _this, Action<MouseEvent> value)
        {
            _this.removeEventListener(MouseEvent.MOUSE_OUT, value.ToFunction(), false);
        }
        #endregion
    }
}
