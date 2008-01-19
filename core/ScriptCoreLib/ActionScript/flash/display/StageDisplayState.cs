using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.display
{
    // http://livedocs.adobe.com/flex/201/langref/flash/display/StageDisplayState.html
    [Script(IsNative = true)]
    public class StageDisplayState
    {
        /// <summary>
        /// Specifies that the Stage is in full-screen mode.
        /// </summary>
        public static readonly string FULL_SCREEN = "fullScreen";
        public static readonly string NORMAL = "normal";
    }
}
