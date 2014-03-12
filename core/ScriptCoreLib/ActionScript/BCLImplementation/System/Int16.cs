using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    [Script(
        Implements = typeof(global::System.Int16),
        ImplementationType = typeof(global::ScriptCoreLib.ActionScript.@int))]
    internal class __Int16
    {
        // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/package.html#parseInt()
        [Script(OptimizedCode = "return parseInt(e);")]
        static public short Parse(string e)
        {
            return default(short);
        }

        [Script(DefineAsStatic = true)]
        public int CompareTo(short value)
        {
            var v = (short)(object)this;

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
            if (!(value is short))
            {
                throw new ArgumentException("MustBeInt16");
            }


            return CompareTo((short)value);
        }





    }
}
