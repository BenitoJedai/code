using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    [Script(
        Implements = typeof(global::System.Int64),
        ImplementationType = typeof(global::ScriptCoreLib.ActionScript.@int))]
    internal class __Int64
    {
        // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/package.html#parseInt()
        [Script(OptimizedCode = "return parseInt(e);")]
        static public long Parse(string e)
        {
            return default(long);
        }

        [Script(DefineAsStatic = true)]
        public int CompareTo(long value)
        {
            var v = (long)(object)this;

            if (v < value)
            {
                return -1;
            }
            if (v > value)
            {
                return 1;
            }
            return 0;
        }

 


        [Script(DefineAsStatic = true)]
        public int CompareTo(object value)
        {
            if (value == null)
            {
                return 1;
            }
            if ((value is long))
            {
                return CompareTo((long)value);
            }


            throw new ArgumentException("MustBeInt32");
        }





    }
}
