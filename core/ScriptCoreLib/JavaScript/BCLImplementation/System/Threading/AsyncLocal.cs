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
		// https://github.com/StephenCleary/Presentations/blob/57ff9987955ae098ddfd4b4c46201e0eeeeae49f/Intro%20to%20Async/Intro%20to%20Async%20-%2016.9.pptx

		// tested by?
		// X:\jsc.svn\examples\javascript\async\test\TestAsyncLocal\TestAsyncLocal\ApplicationControl.cs


		// the context switch needs to reattach the handler on the worker side
		public Action<__AsyncLocalValueChangedArgs<T>> InternalOnValueChanged;

		public __AsyncLocal(Action<__AsyncLocalValueChangedArgs<T>> valueChangedHandler)
		{
			this.InternalOnValueChanged = valueChangedHandler;
		}

		// AsyncLocalValueChangedArgs
		// https://msdn.microsoft.com/en-us/library/dn906269(v=vs.110).aspx
		void __IAsyncLocal.OnValueChanged(object previousValue, object currentValue, bool contextChanged)
		{
			// it is time to notify the user application of a value change, or a context switch..

			this.InternalOnValueChanged(
				new __AsyncLocalValueChangedArgs<T>((T)previousValue, (T)currentValue, contextChanged)
			);


		}
	}
}
