using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.system
{
    // http://livedocs.adobe.com/flex/201/langref/flash/system/System.html
    [Script(IsNative = true)]
    public class System
    {
        #region Properties
        /// <summary>
        /// [static][read-only] The currently installed system IME.
        /// </summary>
        public static IME ime { get; private set; }

        /// <summary>
        /// [static][read-only] The amount of memory (in bytes) currently in use by Adobe® Flash® Player.
        /// </summary>
        public static uint totalMemory { get; private set; }

        /// <summary>
        /// [static] A Boolean value that tells Flash Player which code page to use to interpret external text files.
        /// </summary>
        public static bool useCodePage { get; set; }

        #endregion

    }
}
