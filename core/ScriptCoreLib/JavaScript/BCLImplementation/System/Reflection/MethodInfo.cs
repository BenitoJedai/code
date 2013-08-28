using ScriptCoreLib.JavaScript.DOM;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection
{
    [Script(Implements = typeof(global::System.Reflection.MethodInfo))]
    public class __MethodInfo : __MethodBase
    {
        // Error	2	Inconsistent accessibility: base class 'ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection.__MethodBase' is less accessible than class 'ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection.__MethodInfo'	X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Reflection\MethodInfo.cs	8	18	ScriptCoreLib


        // string instead?
        // prototyp + string?
        // IFunction?
        public string MethodToken;

        public IFunction InternalMethodReference;


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

        public override string ToString()
        {
            // is jsc setting it to be string for methods?

            return new { MethodToken }.ToString();
        }


        // should delegates call here instead of directly calling?
        // should it be inlined?

        public override object InternalInvoke(object obj, object[] parameters)
        {



            return InternalMethodReference.apply(obj, parameters);
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
