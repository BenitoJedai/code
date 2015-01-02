using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib.JavaScript.Runtime;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/single.cs
    // https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/Single.cs

    [Script(Implements = typeof(global::System.Single))]
    internal class __Single
    {

        [Script(OptimizedCode = "return parseFloat(e);")]
        static public __Single Parse(string e)
        {
            return default(__Single);
        }


        [Script(DefineAsStatic = true)]
        public int CompareTo(__Single e)
        {
            return Expando.Compare(this, e);
        }
    }
}

