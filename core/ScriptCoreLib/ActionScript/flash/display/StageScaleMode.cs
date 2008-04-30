using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.display
{
    // http://livedocs.adobe.com/flex/3/langref/flash/display/StageScaleMode.html
    [Script(IsNative=true)]
    public static class StageScaleMode
    {
        #region Constants
        /// <summary>
        /// [static] Specifies that the entire Adobe® Flash® application be visible in the specified area without trying to preserve the original aspect ratio.
        /// </summary>
        public static readonly string EXACT_FIT = "exactFit";

        /// <summary>
        /// [static] Specifies that the entire Flash application fill the specified area, without distortion but possibly with some cropping, while maintaining the original aspect ratio of the application.
        /// </summary>
        public static readonly string NO_BORDER = "noBorder";

        /// <summary>
        /// [static] Specifies that the size of the Flash application be fixed, so that it remains unchanged even as the size of the player window changes.
        /// </summary>
        public static readonly string NO_SCALE = "noScale";

        /// <summary>
        /// [static] Specifies that the entire Flash application be visible in the specified area without distortion while maintaining the original aspect ratio of the application.
        /// </summary>
        public static readonly string SHOW_ALL = "showAll";

        #endregion

    }
}
