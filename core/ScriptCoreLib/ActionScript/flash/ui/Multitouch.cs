using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.ui
{
    // http://help.adobe.com/en_US/FlashPlatform/beta/reference/actionscript/3/flash/ui/Multitouch.html
    [Script(IsNative = true)]
    public static class Multitouch
    {
        #region Properties
        /// <summary>
        /// [static] Identifies the multi-touch mode for touch and gesture event handling.
        /// </summary>
        public static string inputMode { get; set; }

        /// <summary>
        /// [static] [read-only] The maximum number of concurrent touch points supported by the current environment.
        /// </summary>
        public static int maxTouchPoints { get; private set; }

        /// <summary>
        /// [static] [read-only] A Vector array (a typed array of string values) of multi-touch contact types supported in the current environment.
        /// </summary>
        public static string[] /* jsc does not support Vector.<String> ? */ supportedGestures { get; private set; }

        /// <summary>
        /// [static] [read-only] Indicates whether the current environment supports gesture input, such as rotating two fingers around a touch screen.
        /// </summary>
        public static bool supportsGestureEvents { get; private set; }

        /// <summary>
        /// [static] [read-only] Indicates whether the current environment supports basic touch input, such as a single finger tap.
        /// </summary>
        public static bool supportsTouchEvents { get; private set; }

        #endregion

    }
}
