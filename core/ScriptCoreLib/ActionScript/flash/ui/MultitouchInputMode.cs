using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.ui
{
    // http://help.adobe.com/en_US/FlashPlatform/beta/reference/actionscript/3/flash/ui/MultitouchInputMode.html
    [Script(IsNative = true)]
    public static class MultitouchInputMode
    {
        #region Constants
        /// <summary>
        /// [static] Specifies that TransformGestureEvent, PressAndTapGestureEvent, and GestureEvent events are dispatched for the related user interaction supported by the current environment, and other touch events (such as a simple tap) are interpreted as mouse events.
        /// </summary>
        public static readonly string GESTURE = "gesture";

        /// <summary>
        /// [static] Specifies that all user contact with a touch-enabled device is interpreted as a type of mouse event.
        /// </summary>
        public static readonly string NONE = "none";

        /// <summary>
        /// [static] Specifies that events are dispatched only for basic touch events, such as a single finger tap.
        /// </summary>
        public static readonly string TOUCH_POINT = "touchPoint";

        #endregion

    }
}
