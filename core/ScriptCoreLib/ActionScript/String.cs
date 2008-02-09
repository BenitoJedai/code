using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript
{
    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/String.html
    [Script(IsNative=true)]
    public class String
    {
        /// <summary>
        /// Splits a String object into an array of substrings by dividing it wherever the specified delimiter parameter occurs.
        /// </summary>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        public string[] split(object delimiter)
        {
            return default(string[]);
        }


        public string replace(RegExp Trim, string p)
        {
            return default(string);
        }
    }
}
