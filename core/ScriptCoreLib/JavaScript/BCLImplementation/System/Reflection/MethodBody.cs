using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection
{
	// http://referencesource.microsoft.com/#mscorlib/system/reflection/MethodBody.cs
	// https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Reflection/MethodBody.cs
	// https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/Reflection/MethodBody.cs
	// https://github.com/kswoll/WootzJs/blob/master/WootzJs.Runtime/Reflection/MethodBody.cs


	[Script(Implements = typeof(global::System.Reflection.MethodBody))]
	public class __MethodBody
	{
		public virtual byte[] GetILAsByteArray()
		{
			// X:\jsc.svn\examples\javascript\test\TestEditAndContinueWithColor\TestEditAndContinueWithColor\Application.cs
			// can we update js code on the fly via ENC?
			// would it mean jsc/glsl analyzer would need to be restartable?

			// jsc would need to detect debugger unpause, then do a full reanalysis of IL inprocess
			// once ENC changes are detected, patches could be prepared for a client side

			return null;
		}
	}
}
