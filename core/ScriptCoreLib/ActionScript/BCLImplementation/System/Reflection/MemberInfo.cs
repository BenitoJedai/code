using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Reflection
{
    [Script(Implements = typeof(global::System.Reflection.MemberInfo))]
    public abstract class __MemberInfo
    {
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Reflection\MemberInfo.cs


        //V:\web\ScriptCoreLib\ActionScript\BCLImplementation\System\Reflection\__MethodInfo.as(20): col: 29 Error: Overriding a function that is not marked for override.

        //        public function get Name():String
        //                            ^

        //V:\web\ScriptCoreLib\ActionScript\BCLImplementation\System\Reflection\__MethodInfo.as(25): col: 29 Error: Overriding a function that is not marked for override.

        //        public function get DeclaringType():__Type
        //                            ^



        public virtual Type DeclaringType { get { return InternalGetDeclaringType(); } }
        public virtual Type InternalGetDeclaringType() { return default(Type); }

        public virtual string Name { get { return InternalGetName(); } }
        public virtual string InternalGetName() { return default(string); }


    }
}
