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


        public object _Target;
        public global::System.IntPtr _Method;



        //public Function InternalFunctionPointer;

        public override string ToString()
        {
            return new { _Target, _Method }.ToString();
        }


        public static implicit operator MethodInfo(__MethodInfo e)
        {
            return (MethodInfo)(object)e;
        }
    }
}
