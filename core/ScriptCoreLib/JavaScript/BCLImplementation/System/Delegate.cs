using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection;
using ScriptCoreLib.JavaScript.DOM;
using System.Reflection;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/delegate.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System/Delegate.cs
    // https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/Delegate.cs

    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Delegate.cs
    // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Delegate.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Delegate.cs
    // X:\jsc.svn\core\ScriptCoreLibNative\ScriptCoreLibNative\BCLImplementation\System\Delegate.cs

    [Script(Implements = typeof(global::System.Delegate))]
    internal class __Delegate
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201308/20130825

        // script: error JSC1000: No implementation found for this native method, please implement [static System.Delegate.op_Equality(System.Delegate, System.Delegate)]

        [ScriptDelegateDataHint(ScriptDelegateDataHintAttribute.FieldType.Target)]
        public object InternalTarget;

        public object Target { get { return this.InternalTarget; } }


        // public MethodInfo Method { get; }
        // Method: "BAAABm4i9DaI0uFGgA1UPA"
        [ScriptDelegateDataHint(ScriptDelegateDataHintAttribute.FieldType.Method)]
        // string instead?
        public global::System.IntPtr InternalMethod;

        public IFunction InternalMethodReference;


        // X:\jsc.svn\examples\javascript\test\TestIDLDelegateToFunction\TestIDLDelegateToFunction\Class1.cs
        [method: ScriptDelegateDataHint(ScriptDelegateDataHintAttribute.FieldType.AsFunction)]
        [Obsolete("called by the compiler")]
        public static IFunction AsFunction(__Delegate x)
        {
            if (x == null)
                return null;

            return x.InvokePointer;
        }


        // TODO: dom events and delay events do not support truly multiple targets
        public IFunction InvokePointer
        {

            get
            {
                // called by
                // X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\IFunction.cs

                if (InternalMethodReference == null)
                    InternalMethodReference = InternalGetAsyncInvoke(InternalTarget, InternalMethod);

                return InternalMethodReference;
            }
        }

        // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Reflection\MethodInfo.cs
        public global::System.Reflection.MethodInfo Method
        {
            get
            {
                // for now, this will only work with static methods
                // tested by x:\jsc.svn\examples\javascript\Test\TestThreadStart\TestThreadStart\Application.cs

                //global::System.Runtime.InteropServices.Expando
                //ScriptCoreLib.JavaScript.Runtime.Expando.

                var MethodToken = (string)(object)this.InternalMethod;

                if (InternalMethodReference == null)
                    InternalMethodReference = IFunction.Of(MethodToken);

                var m = new __MethodInfo
                {
                    InternalMethodToken = MethodToken,
                    InternalMethodReference = InternalMethodReference
                };

                return (global::System.Reflection.MethodInfo)(object)m;
            }
        }




        //public __Delegate()
        //{

        //}

        // CLR, you using string?
        //  protected Delegate(object target, string method);
        public __Delegate(object e, global::System.IntPtr p)
        {
            // X:\jsc.svn\examples\javascript\WebWorkerExperiment\WebWorkerExperiment\Application.cs
            //if (e == null)
            //    e = Native.Window;

            InternalTarget = e;
            InternalMethod = p;
        }



        // X:\jsc.svn\examples\javascript\test\TestIDLDelegateToFunction\TestIDLDelegateToFunction\Class1.cs
        // special!
        [Script(OptimizedCode = "return function() { return o[p].apply(o, arguments); }")]
        internal static IFunction InternalGetAsyncInvoke(object o, global::System.IntPtr p)
        {
            return default(IFunction);
        }


        #region Combine
        public static __Delegate Combine(__Delegate a, __Delegate b)
        {
            if ((object)a == null)
            {
                return b;
            }
            if ((object)b == null)
            {
                return a;
            }

            return a.CombineImpl(b);
        }

        protected virtual __Delegate CombineImpl(__Delegate d)
        {
            throw new global::System.Exception("use MulticastDelegate instead");
        }

        public static __Delegate Remove(__Delegate source, __Delegate value)
        {
            if (source == null)
            {
                return null;
            }
            if (value == null)
            {
                return source;
            }
            return source.RemoveImpl(value);
        }

        protected virtual __Delegate RemoveImpl(__Delegate d)
        {
            throw new global::System.Exception("use MulticastDelegate instead");
        }
        #endregion


        #region IsEqual
        public override bool Equals(object obj)
        {
            return IsEqual(this, (BCLImplementation.System.__Delegate)obj);

        }


        public static bool IsEqual(__Delegate a, __Delegate b)
        {
            // X:\jsc.svn\examples\javascript\Test\TestWebSQLDatabase\TestWebSQLDatabase\Application.cs

            if ((object)a == null)
                if ((object)b == null)
                    return true;
                else
                    return false;

            if ((object)b == null)
                return false;

            if (a.InternalMethod == b.InternalMethod)
                if (a.InternalTarget == b.InternalTarget)
                    return true;

            return false;
        }

        // a bug if the operator itself compares to nulls

        public static bool operator ==(__Delegate a, __Delegate b)
        {
            return IsEqual(a, b);
        }

        public static bool operator !=(__Delegate a, __Delegate b)
        {
            return !IsEqual(a, b);
        }

        public override int GetHashCode()
        {
            return default(int);
        }
        #endregion




        public static Delegate CreateDelegate(Type type, object firstArgument, global::System.Reflection.MethodInfo method)
        {
            // X:\jsc.svn\examples\javascript\async\Test\TestDelegateObjectScopeInspection\TestDelegateObjectScopeInspection\Application.cs

            //   firstArgument:
            //     The object to which the delegate is bound, or null to treat method as static

            // can we actually call the type.ctor?

            __MethodInfo m = method;

            // um. we are marking it as IntPtr but actually it seems we are using string.

            //var MethodToken = (string)(object)this.InternalMethod;
            // reverse of .Method
            var xIntPtr = (IntPtr)(object)m.InternalMethodToken;

            // [0] = {Void .ctor(System.Object, IntPtr)}
            //var yy = Activator.CreateInstance(typeof(Func<string>),
            //    nRow,
            //    y.Method
            //);

            // can we call CreateInstance with args?
            var withType = Activator.CreateInstance(
                type,
                firstArgument,
                xIntPtr
            );


            //var typeless = new __MulticastDelegate(
            //    firstArgument,
            //    i
            //);


            return (MulticastDelegate)withType;
        }


        public static implicit operator __Delegate(Delegate e)
        {
            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\IFunction.cs
            return (__Delegate)(object)e;
        }
    }

}
