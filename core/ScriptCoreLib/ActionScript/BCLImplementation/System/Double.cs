using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    [Script(
        Implements = typeof(global::System.Double),
        ImplementationType = typeof(global::ScriptCoreLib.ActionScript.Number))]
    internal class __Double
    {
        // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/package.html#parseInt()
        [Script(OptimizedCode = "return parseFloat(e);")]
        static public double Parse(string e)
        {
            return default(double);
        }

        [Script(DefineAsStatic = true)]
        public int CompareTo(double value)
        {
            var v = (double)(object)this;

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
            if (!(value is double))
            {
                throw new ArgumentException("MustBeDouble");
            }


            return CompareTo((double)value);
        }





    }
}
