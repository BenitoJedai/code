using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.text
{
    // http://livedocs.adobe.com/flex/3/langref/flash/text/TextSnapshot.html
    [Script(IsNative=true)]
    public class TextSnapshot
    {
        #region Properties
        /// <summary>
        /// [read-only] The number of characters in a TextSnapshot object.
        /// </summary>
        public int charCount { get; private set; }

        #endregion

    }
}
