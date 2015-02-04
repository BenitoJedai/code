using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices
{
    // http://referencesource.microsoft.com/#mscorlib/system/runtime/compilerservices/runtimehelpers.cs
    // https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Runtime/CompilerServices/RuntimeHelpers.cs

    [Script(Implements = typeof(global::System.Runtime.CompilerServices.RuntimeHelpers))]
	internal class __RuntimeHelpers
	{
        public static object GetObjectValue(object obj)
        {
            return obj;
        }
	}
}
