using System;
using System.Collections.Generic;
using ScriptCoreLib.JavaScript.Runtime;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{

    [Script(Implements = typeof(global::System.Double))]
    internal class __Double
    {

        [Script(OptimizedCode = "return parseFloat(e);")]
        static public __Double Parse(string e)
        {
            return default(__Double);
        }

    

        [Script(OptimizedCode = "return isNaN(d);")]
        public static bool IsNaN(double d)
        {
            return default(bool);

        }

        [Script(DefineAsStatic = true)]
        public int CompareTo(__Double e)
        {
            return Expando.Compare(this, e);
        }
    }
}

