using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.text
{
    // http://help.adobe.com/en_US/FlashPlatform/beta/reference/actionscript/3/flash/text/AntiAliasType.html
    [Script(IsNative = true)]
    public static class AntiAliasType
    {
        #region Constants
        /// <summary>
        /// [static] Sets anti-aliasing to advanced anti-aliasing.
        /// </summary>
        public static readonly string ADVANCED = "advanced";

        /// <summary>
        /// [static] Sets anti-aliasing to the anti-aliasing that is used in Flash Player 7 and earlier.
        /// </summary>
        public static readonly string NORMAL = "normal";

        #endregion

    }
}
