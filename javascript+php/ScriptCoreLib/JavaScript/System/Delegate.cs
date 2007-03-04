using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;

namespace ScriptCoreLib.JavaScript.System
{
    [Script(Implements = typeof(global::System.Delegate))]
    public class DelegateImpl
    {
        [ScriptDelegateDataHint(ScriptDelegateDataHintAttribute.FieldType.Target)]
        public object Target;
        [ScriptDelegateDataHint(ScriptDelegateDataHintAttribute.FieldType.Method)]
        public global::System.IntPtr Method;


        // TODO: dom events and delay events do not support truly multiple targets
        public IFunction InvokePointer;

        public DelegateImpl(object e, global::System.IntPtr p)
        {
            Target = e == null ? Native.Window : e;
            Method = p;

            InvokePointer = InternalGetAsyncInvoke(Target, Method);
        }




        [Script(OptimizedCode="return function(a0, a1) { return o[p](a0, a1); }")]
        internal static IFunction InternalGetAsyncInvoke(object o, global::System.IntPtr p)
        {
            return default(IFunction);
        }

        public static DelegateImpl Combine(DelegateImpl a, DelegateImpl b)
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

        protected virtual DelegateImpl CombineImpl(DelegateImpl d)
        {
            throw new ScriptException("use MulticastDelegate instead");
        }

        public static DelegateImpl Remove(DelegateImpl source, DelegateImpl value)
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

        protected virtual DelegateImpl RemoveImpl(DelegateImpl d)
        {
            if (!d.Equals(this))
            {
                return this;
            }
            return null;
        }

        public override bool Equals(object obj)
        {
            DelegateImpl v = (DelegateImpl)obj;

            return v.Method == this.Method &&
                    v.Target == this.Target;
        }

        public override int GetHashCode()
        {
            return default(int);
        }
    }
}