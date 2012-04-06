using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    using ScriptCoreLib.JavaScript.Runtime;

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

