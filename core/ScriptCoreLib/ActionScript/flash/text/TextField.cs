using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ScriptCoreLib.ActionScript.flash.display;

namespace ScriptCoreLib.ActionScript.flash.text
{
    // http://livedocs.adobe.com/flex/2/langref/flash/text/TextField.html
    [Script(IsNative = true)]
    public class TextField : InteractiveObject
    {
        /// <summary>
        /// A string that is the current text in the text field.
        /// </summary>
        public string text;
    }
}
