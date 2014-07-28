using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/boolean.cs

    [Script(Implements = typeof(global::System.Boolean))]
    internal class __Boolean
    {
        [Script(OptimizedCode=@"return !!e;")]
        public static __Boolean Parse(string e)
        {
            return default(__Boolean);
        }
    }
}
