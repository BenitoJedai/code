using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection
{
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



        public override string Name
        {
            get { throw new NotImplementedException(); }
        }

        public override Type DeclaringType
        {
            get { throw new NotImplementedException(); }
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
    }
}
