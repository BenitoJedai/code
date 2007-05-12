using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    using ScriptCoreLib.JavaScript.Runtime;

    [Script(Implements = typeof(global::System.Double))]
    internal class __Double
    {

        [Script(OptimizedCode = "return parseFloat(e);")]
        static public __Double Parse(string e)
        {
            return default(__Double);
        }


        [Script(DefineAsStatic = true)]
        public int CompareTo(__Double e)
        {
            return Expando.Compare(this, e);
        }
    }
}

