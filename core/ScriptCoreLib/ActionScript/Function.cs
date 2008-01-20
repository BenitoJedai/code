using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript
{
    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/Function.html
    [Script(IsNative = true)]
    public class Function
    {

        /// <summary>
        /// Specifies the value of thisObject to be used within any function that ActionScript calls.
        /// </summary>
        /// <param name="thisObject"></param>
        /// <param name="argArray"></param>
        /// <returns></returns>
        public object apply(object thisObject, Array argArray)
        {
            return default(object);
        }


    }
}
