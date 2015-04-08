using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Threading
{
	// https://msdn.microsoft.com/en-us/library/dn906269(v=vs.110).aspx
	// https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Threading/AsyncLocal.cs

	[Script(ImplementsViaAssemblyQualifiedName = "System.Threading.AsyncLocalValueChangedArgs`1")]
	//[Script(Implements = typeof(global::System.Threading.AsyncLocal<>))]
	internal class __AsyncLocalValueChangedArgs<T>
	{
		// tested by?

		public T PreviousValue { get; private set; }
		public T CurrentValue { get; private set; }


		public bool ThreadContextChanged { get; private set; }

		/// <summary>
		///  
		/// </summary>
		/// <param name="previousValue"></param>
		/// <param name="currentValue"></param>
		/// <param name="contextChanged"></param>
		public __AsyncLocalValueChangedArgs(T previousValue, T currentValue, bool contextChanged)
        {
			PreviousValue = previousValue;
			CurrentValue = currentValue;
			ThreadContextChanged = contextChanged;
		}
	}
}
