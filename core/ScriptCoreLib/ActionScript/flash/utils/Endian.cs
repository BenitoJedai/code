using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.utils
{
    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/flash/utils/Endian.html
    [Script(IsNative = true)]
    public class Endian
    {
        #region Constants
        /// <summary>
        /// [static] Indicates the most significant byte of the multibyte number appears first in the sequence of bytes.
        /// </summary>
        public static readonly string BIG_ENDIAN = "bigEndian";

        /// <summary>
        /// [static] Indicates the least significant byte of the multibyte number appears first in the sequence of bytes.
        /// </summary>
        public static readonly string LITTLE_ENDIAN = "littleEndian";

        #endregion

    }
}
