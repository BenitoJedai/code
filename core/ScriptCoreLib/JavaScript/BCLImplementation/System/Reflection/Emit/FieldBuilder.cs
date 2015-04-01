using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection.Emit
{
	// https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Reflection/Emit/FieldBuilder.cs

	[Script(Implements = typeof(global::System.Reflection.Emit.FieldBuilder))]
	public sealed class __FieldBuilder : __FieldInfo
	{
		// when would we be able to hop into a dynamic method?
	}
}
