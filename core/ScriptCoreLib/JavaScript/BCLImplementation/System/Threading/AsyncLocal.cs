using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Threading
{
	// https://msdn.microsoft.com/en-us/library/dn906268%28v=vs.110%29.aspx
	// https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Threading/AsyncLocal.cs

	[Script(ImplementsViaAssemblyQualifiedName = "System.Threading.AsyncLocal`1")]
	//[Script(Implements = typeof(global::System.Threading.AsyncLocal<>))]
	internal class __AsyncLocal<T> : __IAsyncLocal
	{
		// tested by?

		public __AsyncLocal(Action<__AsyncLocalValueChangedArgs<T>> valueChangedHandler)
		{

		}

		// AsyncLocalValueChangedArgs
		// https://msdn.microsoft.com/en-us/library/dn906269(v=vs.110).aspx
		void __IAsyncLocal.OnValueChanged(object previousValue, object currentValue, bool contextChanged)
		{
		}
	}
}
