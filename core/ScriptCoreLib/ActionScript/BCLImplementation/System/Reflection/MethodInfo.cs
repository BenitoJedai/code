using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Reflection
{
    [Script(Implements = typeof(global::System.Reflection.MethodInfo))]
    internal class __MethodInfo : __MethodBase
    {
        // X:\jsc.svn\examples\actionscript\Test\TestThreadStart\TestThreadStart\ApplicationSprite.cs


        //public object _Target;
        public __IntPtr _Method;


        public override string Name
        {
            get { return _Method.FunctionToken_MethodName; }

        }

        public override Type DeclaringType
        {
            get { return Type.GetType(_Method.FunctionToken_TypeFullName); }
        }


        //public Function InternalFunctionPointer;

        public override string ToString()
        {
            return new { _Method, Name, DeclaringType }.ToString();
        }


        public static implicit operator MethodInfo(__MethodInfo e)
        {
            return (MethodInfo)(object)e;
        }
    }
}
