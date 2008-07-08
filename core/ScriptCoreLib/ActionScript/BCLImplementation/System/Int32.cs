using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    [Script(
        Implements = typeof(global::System.Int32),
        ImplementationType = typeof(global::ScriptCoreLib.ActionScript.@int))]
    internal class __Int32
    {
        // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/package.html#parseInt()
        [Script(OptimizedCode = "return parseInt(e);")]
        static public int Parse(string e)
        {
            return default(int);
        }





    }
}
