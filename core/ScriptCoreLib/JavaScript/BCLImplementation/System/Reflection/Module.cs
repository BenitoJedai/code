using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection
{
	// http://referencesource.microsoft.com/#mscorlib/system/reflection/Module.cs
	// https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Reflection/Module.cs
	// https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/Reflection/Module.cs
	// https://github.com/kswoll/WootzJs/blob/master/WootzJs.Runtime/Reflection/Module.cs


	[Script(Implements = typeof(global::System.Reflection.Module))]
	public class __Module
	{
		public virtual string ResolveString(int metadataToken)
		{
			// X:\jsc.svn\examples\javascript\test\TestEditAndContinueWithColor\TestEditAndContinueWithColor\Application.cs
			// can we detet if server debugger updates a ldstr ?

			return "";
		}
	}
}
