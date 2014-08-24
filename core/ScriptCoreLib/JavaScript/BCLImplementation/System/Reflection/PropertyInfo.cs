using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection
{
    // http://referencesource.microsoft.com/#mscorlib/system/reflection/propertyinfo.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Reflection/PropertyInfo.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Reflection\PropertyInfo.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Reflection\PropertyInfo.cs


    [Script(Implements = typeof(global::System.Reflection.PropertyInfo))]
    public class __PropertyInfo : __MemberInfo
    {
        // how will we teach js/java about property meta data?



        //02000051 ScriptCoreLib.Query.Experimental.QueryExpressionBuilder+SQLWriter`1+<>c__DisplayClass2
        //no implementation for System.Reflection.PropertyInfo bfdf1f57-230d-394a-b773-d9ec58cbef9a
        //script: error JSC1000: No implementation found for this native method, please implement [static System.Reflection.PropertyInfo.op_Inequality(System.Reflection.PropertyInfo, System.Reflection.PropertyInfo)]

        public static bool operator !=(__PropertyInfo left, __PropertyInfo right)
        {
            return (object)left != (object)right;
        }

        public static bool operator ==(__PropertyInfo left, __PropertyInfo right)
        {
            return (object)left == (object)right;
        }

        public virtual object GetValue(object obj, object[] index)
        {
            return null;
        }

        //        02000051 ScriptCoreLib.Query.Experimental.QueryExpressionBuilder+SQLWriter`1+<>c__DisplayClass2
        //arg[0] is typeof System.Object
        //arg[1] is typeof System.Object[]
        //script: error JSC1000: No implementation found for this native method, please implement [System.Reflection.PropertyInfo.GetValue(System.Object, System.Object[])]


        public override Type DeclaringType
        {
            get { throw new NotImplementedException(); }
        }

        public override string Name
        {
            get { throw new NotImplementedException(); }
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
