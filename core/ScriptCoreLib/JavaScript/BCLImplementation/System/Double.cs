using System;
using System.Collections.Generic;
using ScriptCoreLib.JavaScript.Runtime;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/double.cs
    // https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Double.cs
    // https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/Double.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Double.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Double.cs

    [Script(Implements = typeof(double))]
    internal class __Double
    {
        // double[]
        // X:\jsc.svn\examples\javascript\test\TestDoubleArray\TestDoubleArray\Class1.cs


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

