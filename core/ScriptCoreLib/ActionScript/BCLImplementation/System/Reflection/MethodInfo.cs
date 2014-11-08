using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Reflection
{
    [Script(Implements = typeof(global::System.Reflection.MethodInfo))]
    public class __MethodInfo : __MethodBase
    {
        // X:\jsc.svn\examples\actionscript\Test\TestThreadStart\TestThreadStart\ApplicationSprite.cs


        //public object _Target;
        public __IntPtr _Method;



        [Script(OptimizedCode = "return o[n];")]
        private static Function GetFunctionPointer(object o, string n)
        {
            return default(Function);
        }

        // used by ?
        public Function FunctionPointer
        {
            get
            {

                __IntPtr p = this._Method;

                if (p.FunctionToken == null)
                {
                    // catch {{ err = ReferenceError: Error #1069: Property Invoke_6d788eff_06000013 not found on ScriptCoreLib.ActionScript.BCLImplementation.System.__Type and there is no default 

                    if (p.FunctionToken_MethodName != null)
                    {
                        p.FunctionToken = GetFunctionPointer(
                            __Type.getDefinitionByName(p.FunctionToken_TypeFullName),
                            p.FunctionToken_MethodName
                            );
                    }
                    //else
                    //{
                    //    p.FunctionToken = GetFunctionPointer(_Target, p.StringToken);
                    //}
                }

                return p.FunctionToken;
            }
        }

        public override object InternalInvoke(object obj, object[] parameters)
        {
            // X:\jsc.svn\examples\actionscript\Test\TestThreadStart\TestThreadStart\ApplicationSprite.cs

            return this.FunctionPointer.apply(obj, (Array)(object)parameters);
        }

        public override string InternalGetName()
        {
            return _Method.FunctionToken_MethodName;
        }

        public override Type InternalGetDeclaringType()
        {
            return Type.GetType(_Method.FunctionToken_TypeFullName);
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

        public static implicit operator __MethodInfo(MethodInfo e)
        {
            return (__MethodInfo)(object)e;
        }
    }
}
