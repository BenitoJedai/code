using ScriptCoreLib.JavaScript.DOM;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection
{
	// http://referencesource.microsoft.com/#mscorlib/system/reflection/methodinfo.cs
	// https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Reflection/MethodInfo.cs
	// https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Reflection/MethodInfo.cs
	// https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/Reflection/MethodInfo.cs
	// https://github.com/kswoll/WootzJs/blob/master/WootzJs.Runtime/Reflection/MethodInfo.cs

	// X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Reflection\MethodInfo.cs
	// X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Reflection\MethodInfo.cs
	// X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Reflection\MethodInfo.cs
	// X:\jsc.svn\core\ScriptCoreLibNative\ScriptCoreLibNative\BCLImplementation\System\Reflection\MethodInfo.cs

	// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/20140705/20140714
	// WootzJs.Runtime\Reflection\MethodInfo.cs
	[Script(Implements = typeof(global::System.Reflection.MethodInfo))]
    public class __MethodInfo : __MethodBase
    {
        // X:\jsc.svn\examples\javascript\async\Test\TestDelegateObjectScopeInspection\TestDelegateObjectScopeInspection\Application.cs

        // Error	2	Inconsistent accessibility: base class 'ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection.__MethodBase' is less accessible than class 'ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection.__MethodInfo'	X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Reflection\MethodInfo.cs	8	18	ScriptCoreLib


        // string instead?
        // prototyp + string?
        // IFunction?


        #region use IntPtr instead?
        public string InternalMethodToken;

        public IFunction InternalMethodReference;
        #endregion


        public override string Name
        {
            get { return InternalMethodToken; }
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
            // we could also show the .displayName ?


            // is jsc setting it to be string for methods?

            return new { InternalMethodToken }.ToString();
        }


        // should delegates call here instead of directly calling?
        // should it be inlined?

        public override object InternalInvoke(object obj, object[] parameters)
        {



            return InternalMethodReference.apply(obj, parameters);
        }

        //script: error JSC1000: No implementation found for this native method, please implement [static System.Reflection.MethodInfo.op_Equality(System.Reflection.MethodInfo, System.Reflection.MethodInfo)]



        public static bool operator !=(__MethodInfo left, __MethodInfo right)
        {
            // we should check token instead?
            return left.Name != right.Name;
        }

        public static bool operator ==(__MethodInfo left, __MethodInfo right)
        {
            return left.Name == right.Name;
        }


        public static implicit operator MethodInfo(__MethodInfo e)
        {
            return (MethodInfo)(object)e;
        }

        public static implicit operator __MethodInfo(MethodInfo e)
        {
            return (__MethodInfo)(object)e;
        }

        public override ParameterInfo[] GetParameters()
        {
            throw new NotImplementedException();
        }

        public override Type DeclaringType
        {
            get
            {
                // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestWebOrderByThenGroupBy\Application.cs

                // where is this method defined?
                // how was the ref taken/ ldtoken?

                return typeof(object);
            }
        }
    }
}
