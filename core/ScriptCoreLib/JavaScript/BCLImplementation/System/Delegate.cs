using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    using ScriptCoreLib.JavaScript.DOM;

    [Script(Implements = typeof(global::System.Delegate))]
    internal class __Delegate
    {
        // script: error JSC1000: No implementation found for this native method, please implement [static System.Delegate.op_Equality(System.Delegate, System.Delegate)]

        [ScriptDelegateDataHint(ScriptDelegateDataHintAttribute.FieldType.Target)]
        public object Target;

        // script: error JSC1000: No implementation found for this native method, please implement [System.Delegate.get_Method()]

        // public MethodInfo Method { get; }
        // Method: "BAAABm4i9DaI0uFGgA1UPA"
        [ScriptDelegateDataHint(ScriptDelegateDataHintAttribute.FieldType.Method)]
        public global::System.IntPtr Method;


        // TODO: dom events and delay events do not support truly multiple targets
        IFunction InvokePointerCache;

        public IFunction InvokePointer
        {
            get
            {
                if (InvokePointerCache == null)
                    InvokePointerCache = InternalGetAsyncInvoke(Target, Method);

                return InvokePointerCache;
            }
        }

        public __Delegate(object e, global::System.IntPtr p)
        {
            // X:\jsc.svn\examples\javascript\WebWorkerExperiment\WebWorkerExperiment\Application.cs
            //if (e == null)
            //    e = Native.Window;

            Target = e;
            Method = p;
        }




        [Script(OptimizedCode = "return function() { return o[p].apply(o, arguments); }")]
        internal static IFunction InternalGetAsyncInvoke(object o, global::System.IntPtr p)
        {
            return default(IFunction);
        }

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

        public override bool Equals(object obj)
        {
            return IsEqual(this, (BCLImplementation.System.__Delegate)obj);

        }


        public static bool IsEqual(__Delegate a, __Delegate b)
        {
            if ((object)a == null)
                return false;

            if ((object)b == null)
                return false;

            if (a.Method == b.Method)
                if (a.Target == b.Target)
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
    }

}
