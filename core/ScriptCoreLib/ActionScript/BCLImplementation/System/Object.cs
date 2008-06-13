using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    [Script(Implements = typeof(object))]
    internal class __Object
    {
        [Script(OptimizedCode = "return a == b;")]
        public static bool ReferenceEquals(object a, object b)
        {
            return default(bool);
        }

        public new virtual string ToString()
        {
            return default(string);
        }

        [Script(DefineAsStatic = true)]
        new public Type GetType()
        {
            return __Type.GetTypeFromValue(this);
        }
    }
}
