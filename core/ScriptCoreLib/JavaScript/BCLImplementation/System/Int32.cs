using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    using ScriptCoreLib.JavaScript.Runtime;

    [Script(Implements = typeof(global::System.Int32))]
    internal class __Int32
    {

        [Script(OptimizedCode = "return parseInt(e);")]
        static public __Int32 Parse(string e)
        {
            return default(__Int32);
        }

        [Script(DefineAsStatic = true)]
        public int CompareTo(__Int32 e)
        {
            return Expando.Compare(this, e);
           
        }
    }
}
