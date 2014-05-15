using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Reflection;

namespace ScriptCoreLibJava.BCLImplementation.System.Reflection
{
    [Script(Implements = typeof(MemberInfo))]
    internal abstract class __MemberInfo
    {
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Reflection\MemberInfo.cs

        public abstract string Name { get; }

        public abstract Type DeclaringType { get; }


        //Implementation not found for type import :
        //type: System.Reflection.MemberInfo
        //method: Boolean op_Inequality(System.Reflection.MemberInfo, System.Reflection.MemberInfo)
        //Did you forget to add the [Script] attribute?
        //Please double check the signature!

        // X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationWebService.cs

        public static bool operator !=(__MemberInfo left, __MemberInfo right)
        {
            return (object)left != (object)right;
        }

        public static bool operator ==(__MemberInfo left, __MemberInfo right)
        {
            return (object)left == (object)right;

        }
    }
}
