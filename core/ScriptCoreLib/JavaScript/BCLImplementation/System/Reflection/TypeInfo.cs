using ScriptCoreLib.Shared.BCLImplementation.System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection
{
	// https://msdn.microsoft.com/en-us/library/system.reflection.typeinfo
	// https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Reflection/TypeInfo.cs

	//[Script(Implements = typeof(global::System.Reflection.TypeInfo))]
	public sealed class __TypeInfo : __Type, __IReflectableType
	{
		// whats this good for?
		// A TypeInfo object represents the type definition itself, whereas a Type object represents a reference to the type definition. 
		// Getting a TypeInfo object forces the assembly that contains that type to load. In comparison, you can manipulate 
		// Type objects without necessarily requiring the runtime to load the assembly they reference.

		// needed for ENC?
	}
}
