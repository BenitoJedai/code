using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection.Emit
{
	// https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Reflection/Emit/DynamicMethod.cs

	[Script(Implements = typeof(global::System.Reflection.Emit.DynamicMethod))]
	public sealed class __DynamicMethod : __MethodInfo
	{
		// when would we be able to hop into a dynamic method?
	}
}
