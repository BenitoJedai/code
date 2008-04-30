using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.ui
{
    // http://livedocs.adobe.com/flex/3/langref/flash/ui/KeyLocation.html
    [Script(IsNative=true)]
    public static class KeyLocation
    {
        #region Constants
        /// <summary>
        /// [static] Indicates the key activated is in the left key location (there is more than one possible location for this key).
        /// </summary>
        public static readonly uint LEFT = 1;

        /// <summary>
        /// [static] Indicates the key activation originated on the numeric keypad or with a virtual key corresponding to the numeric keypad.
        /// </summary>
        public static readonly uint NUM_PAD = 3;

        /// <summary>
        /// [static] Indicates the key activated is in the right key location (there is more than one possible location for this key).
        /// </summary>
        public static readonly uint RIGHT = 2;

        /// <summary>
        /// [static] Indicates the key activation is not distinguished as the left or right version of the key, and did not originate on the numeric keypad (or did not originate with a virtual key corresponding to the numeric keypad).
        /// </summary>
        public static readonly uint STANDARD = 0;

        #endregion

    }
}
