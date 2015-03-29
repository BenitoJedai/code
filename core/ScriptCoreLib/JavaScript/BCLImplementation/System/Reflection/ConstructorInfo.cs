using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection
{
	// http://referencesource.microsoft.com/#mscorlib/system/reflection/ConstructorInfo.cs
	// https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/Reflection/ConstructorInfo.cs
	// https://github.com/kswoll/WootzJs/blob/master/WootzJs.Runtime/Reflection/ConstructorInfo.cs

	[Script(Implements = typeof(global::System.Reflection.ConstructorInfo))]
    public class __ConstructorInfo : __MethodBase
    {
        // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Reflection\ConstructorInfo.cs

        // script: error JSC1000: No implementation found for this native method, please implement [static System.Reflection.ConstructorInfo.op_Inequality(System.Reflection.ConstructorInfo, System.Reflection.ConstructorInfo)]

        //public static bool operator !=(__ConstructorInfo left, __ConstructorInfo right)
        //{
        //    return false;
        //}

        //public static bool operator ==(__ConstructorInfo left, __ConstructorInfo right)
        //{
        //    return false;
        //}


        //script: error JSC1000: No implementation found for this native method, please implement [System.Reflection.ConstructorInfo.Invoke(System.Object[])]
        public object Invoke(object[] parameters)
        {
            // X:\jsc.svn\examples\javascript\LINQ\ComplexQueryExperiment\ComplexQueryExperiment\Application.cs
            // Activator?

            return null;
        }

        public override string Name
        {
            get { return ".ctor"; }
        }


        public Type InternalDeclaringType;
        public Type[] InternalParameterTypes;

        public override Type DeclaringType
        {
            get { return InternalDeclaringType; }
        }

        public override global::System.Reflection.ParameterInfo[] GetParameters()
        {
            throw new NotImplementedException();
        }

        public override object InternalInvoke(object obj, object[] parameters)
        {
            throw new NotImplementedException();
        }

        public override object[] GetCustomAttributes(Type x, bool inherit)
        {
            throw new NotImplementedException();
        }

        public override object[] GetCustomAttributes(bool inherit)
        {
            throw new NotImplementedException();
        }

        public static implicit operator ConstructorInfo(__ConstructorInfo x)
        {
            return (ConstructorInfo)(object)x;
        }
    }
}
