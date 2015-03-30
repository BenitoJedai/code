using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection
{
	// http://referencesource.microsoft.com/#mscorlib/system/reflection/methodbase.cs
	// https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Reflection/MethodBase.cs
	// https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/Reflection/MethodBase.cs
	// https://github.com/kswoll/WootzJs/blob/master/WootzJs.Runtime/Reflection/MethodBase.cs
	// https://github.com/erik-kallen/SaltarelleCompiler/blob/develop/Runtime/CoreLib/Reflection/MethodBase.cs


	[Script(Implements = typeof(global::System.Reflection.MethodBase))]
	public abstract class __MethodBase : __MemberInfo
	{
		// https://github.com/dotnet/coreclr/blob/master/Documentation/method-descriptor.md

		// X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Reflection\MethodBase.cs

		public abstract ParameterInfo[] GetParameters();


		public abstract object InternalInvoke(object obj, object[] parameters);

		public object Invoke(object obj, object[] parameters)
		{
			return InternalInvoke(obj, parameters);
		}

		// whats with IsDynamicallyInvokable



		public static MethodBase GetCurrentMethod()
		{
			// stack would know where we are...
			// X:\jsc.svn\examples\javascript\test\TestEditAndContinueWithColor\TestEditAndContinueWithColor\Application.cs

			//wootz:             // This will be a fun one to try to implement.  Probably just need to instrument each method body to 
			// declare a $currentMethod$ variable to Type.prototype.method or Type.method

			// forum in code.

			// didnt javascript provide arguments, callee, caller?

			return null;
		}

		public virtual MethodBody GetMethodBody()
		{
			// ENC databinding?

			// would we allow some methods to be defined in IL?
			// would need runtime IL decompiler to run in webview then?
			// run on worker thread, load async on demand, only if debuger attached to server?

			return null;
		}
	}
}
