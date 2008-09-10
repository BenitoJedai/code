using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    using ScriptCoreLib.JavaScript.DOM;

    [Script(Implements = typeof(global::System.Delegate))]
    public class __Delegate
    {
        [ScriptDelegateDataHint(ScriptDelegateDataHintAttribute.FieldType.Target)]
        public object Target;

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
            Target = e == null ? Native.Window : e;
            Method = p;
        }




        [Script(OptimizedCode="return function(a0, a1) { return o[p](a0, a1); }")]
        internal static IFunction InternalGetAsyncInvoke(object o, global::System.IntPtr p)
        {
            return default(IFunction);
        }

        public static __Delegate Combine(__Delegate a, __Delegate b)
        {
            if (a == null)
            {
                return b;
            }
            if (b == null)
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
            return IsEqual(this,  (BCLImplementation.System.__Delegate)obj );

        }


        public static bool IsEqual(__Delegate a, __Delegate b)
        {
            if ((object)a == null)
                return false;

            if ((object)b == null)
                return false;

            return a.Method == b.Method &&
                    a.Target == b.Target;
        }

        // a bug if the operator itself compares to nulls

        //public static bool operator == (DelegateImpl a, DelegateImpl b)
        //{
        //    return IsEqual(a, b);
        //}

        //public static bool operator != (DelegateImpl a, DelegateImpl b)
        //{
        //    return !IsEqual(a, b);
        //}

        public override int GetHashCode()
        {
            return default(int);
        }
    }

}
