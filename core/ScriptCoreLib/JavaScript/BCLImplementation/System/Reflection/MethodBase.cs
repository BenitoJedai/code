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
    }
}
