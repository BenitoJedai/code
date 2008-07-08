using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    [Script(
        Implements = typeof(global::System.UInt32),
        ImplementationType = typeof(global::ScriptCoreLib.ActionScript.@uint))]
    internal class __UInt32
    {
        // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/package.html#parseInt()
        [Script(OptimizedCode = "return uint(parseInt(e));")]
        static public uint Parse(string e)
        {
            return default(uint);
        }

        [Script(DefineAsStatic = true)]
        public int CompareTo(uint value)
        {
            var v = (uint)(object)this;

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
            if (!(value is uint))
            {
                throw new ArgumentException("MustBeUInt32");
            }


            return CompareTo((uint)value);
        }





    }
}
