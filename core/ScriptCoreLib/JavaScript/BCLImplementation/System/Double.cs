using System;
using System.Collections.Generic;
using ScriptCoreLib.JavaScript.Runtime;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/double.cs
    // https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/Double.cs

    [Script(Implements = typeof(double))]
    internal class __Double
    {
        #region OptimizedCode
        [Script(OptimizedCode = "return parseFloat(e);")]
        static public int parseFloat(string e)
        {
            return default(int);
        }

        [Script(OptimizedCode = "return isNaN(d);")]
        public static bool isNaN(int d)
        {
            return default(bool);

        }
        #endregion




        // script: error JSC1000: No implementation found for this native method, please implement [static System.Double.TryParse(System.String, System.Double&)]
        [Script(DefineAsStatic = true)]
        static public bool TryParse(string e, out double result)
        {
            // tested by
            // X:\jsc.svn\examples\javascript\forms\FormsDataGridViewDeleteRow\FormsDataGridViewDeleteRow\ApplicationControl.cs
            // X:\jsc.svn\examples\javascript\test\TestInlineTryParse\TestInlineTryParse\Application.cs

            //parseInt('s')
            //NaN


            var x = parseFloat(e);
            var nan = isNaN(x);

            if (nan)
                result = 0;
            else
                result = x;

            return !nan;
        }



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

