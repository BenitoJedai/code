using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection
{
	// http://referencesource.microsoft.com/#mscorlib/system/reflection/parameterinfo.cs
	// https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Reflection/ParameterInfo.cs
	// https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/Reflection/ParameterInfo.cs
	// https://github.com/kswoll/WootzJs/blob/master/WootzJs.Runtime/Reflection/ParameterInfo.cs

	// X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Reflection\ParameterInfo.cs
	// X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Reflection\ParameterInfo.cs

	[Script(Implements = typeof(global::System.Reflection.ParameterInfo))]
    public class __ParameterInfo
    {
        //02000051 ScriptCoreLib.Query.Experimental.QueryExpressionBuilder+SQLWriter`1+<>c__DisplayClass2
        //no implementation for System.Reflection.PropertyInfo bfdf1f57-230d-394a-b773-d9ec58cbef9a
        //script: error JSC1000: No implementation found for this native method, please implement [static System.Reflection.PropertyInfo.op_Inequality(System.Reflection.PropertyInfo, System.Reflection.PropertyInfo)]



        public virtual string Name
        {
            get
            {
                return "arg" + this.Position;
            }
        }

        public virtual int Position { get; set; }

        public virtual Type ParameterType { get; set; }

        public static implicit operator ParameterInfo(__ParameterInfo e)
        {
            return (ParameterInfo)(object)e;
        }
    }
}
