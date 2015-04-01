using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
	// http://referencesource.microsoft.com/#mscorlib/system/runtimehandles.cs
	// https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/RuntimeHandles.cs

	// https://github.com/kswoll/WootzJs/blob/master/WootzJs.Runtime/RuntimeTypeHandle.cs
	// https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/RuntimeTypeHandle.cs
	// https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/RuntimeTypeHandle.cs

	[Script(Implements = typeof(global::System.RuntimeTypeHandle))]
    public sealed class __RuntimeTypeHandle
    {
        // X:\jsc.svn\examples\javascript\test\TestTypeHandle\TestTypeHandle\Application.cs

        // is this used?
        public __RuntimeTypeHandle()
        {

        }

        // special method invoked on typeof(Type) statement
        public __RuntimeTypeHandle(IntPtr e)
        {
            this.Value = e;
        }


        public IntPtr Value
        {
            get;
            set;
        }

        public static implicit operator RuntimeTypeHandle(__RuntimeTypeHandle e)
        {
            return (RuntimeTypeHandle)(object)e;
        }
    }
}
