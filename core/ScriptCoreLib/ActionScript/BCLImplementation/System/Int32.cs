using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    [Script(
        Implements = typeof(global::System.Int32),
        ImplementationType = typeof(global::ScriptCoreLib.ActionScript.@int))]
    internal class __Int32
    {
        // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/package.html#parseInt()
        [Script(OptimizedCode = "return parseInt(e);")]
        static public int Parse(string e)
        {
            return default(int);
        }

        [Script(DefineAsStatic = true)]
        public int CompareTo(int value)
        {
            var v = (int)(object)this;

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

            //       if ((value as  int) == null)
            //^

            // X:\jsc.svn\examples\actionscript\test\TestIsInt32\TestIsInt32\Class1.cs
            if ((value is int))
            {
                return CompareTo((int)value);

            }

            throw new ArgumentException("MustBeInt32");
        }





    }
}
