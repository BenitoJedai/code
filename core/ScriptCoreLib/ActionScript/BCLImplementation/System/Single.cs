using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    [Script(
        Implements = typeof(global::System.Single),
        ImplementationType = typeof(global::ScriptCoreLib.ActionScript.Number))]
    internal class __Single
    {
        // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/package.html#parseInt()
        [Script(OptimizedCode = "return parseFloat(e);")]
        static public float Parse(string e)
        {
            return default(float);
        }

        [Script(DefineAsStatic = true)]
        public int CompareTo(float value)
        {
            var v = (float)(object)this;

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

        // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/package.html#isNaN()
        [Script(OptimizedCode = "return Number.NaN;")]
        static public float NaN;


        [Script(DefineAsStatic = true)]
        public int CompareTo(object value)
        {
            if (value == null)
            {
                return 1;
            }

            // X:\jsc.svn\examples\actionscript\test\TestIsInt32\TestIsInt32\Class1.cs
            if ((value is float))
            {
                return CompareTo((float)value);
            }


            throw new ArgumentException("MustBeDouble");
        }





    }
}
