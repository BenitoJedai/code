using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/double.cs
    // https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Double.cs
    // https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/Double.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Double.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Double.cs


	[Script(Implements = typeof(global::System.Double),
		ImplementationType = typeof(java.lang.Double))]
	internal class __Double
	{
        // X:\jsc.svn\examples\javascript\Test\TestInlineTryParse\TestInlineTryParse\Application.cs

		[Script(ExternalTarget = "parseDouble")]
		public static double Parse(string e)
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
	}
}
