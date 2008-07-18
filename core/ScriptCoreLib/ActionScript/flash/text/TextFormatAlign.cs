using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.text
{
    // http://livedocs.adobe.com/flex/3/langref/flash/text/TextFormatAlign.html
    [Script(IsNative = true)]
    public class TextFormatAlign
    {
        #region Constants
        /// <summary>
        /// [static] Constant; centers the text in the text field.
        /// </summary>
        public static readonly string CENTER = "center";

        /// <summary>
        /// [static] Constant; justifies text within the text field.
        /// </summary>
        public static readonly string JUSTIFY = "justify";

        /// <summary>
        /// [static] Constant; aligns text to the left within the text field.
        /// </summary>
        public static readonly string LEFT = "left";

        /// <summary>
        /// [static] Constant; aligns text to the right within the text field.
        /// </summary>
        public static readonly string RIGHT = "right";

        #endregion

    }
}
