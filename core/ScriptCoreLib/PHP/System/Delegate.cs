using ScriptCoreLib.PHP;
using ScriptCoreLib.PHP.Runtime;

namespace ScriptCoreLib.PHP.System
{
    [Script(Implements = typeof(global::System.Delegate))]
    internal class DelegateImpl
    {
        [ScriptDelegateDataHint(ScriptDelegateDataHintAttribute.FieldType.Target)]
        public object Target;

        [ScriptDelegateDataHint(ScriptDelegateDataHintAttribute.FieldType.Method)]
        public global::System.IntPtr Method;



        public DelegateImpl(object e, global::System.IntPtr p)
        {
            Target = e;
            Method = p;

        }



        //#region InternalInvoke
        //internal void InternalInvoke()
        //{

        //    InternalInvoke(Target, Method);


        //}

        //internal void InternalInvokeVar<T>(T e)
        //{
        //    InternalInvokeVar(Target, Method, e);
        //}

        //internal void InternalInvokeVar<var0, var1>(var0 e, var1 p)
        //{
        //    InternalInvokeVar(Target, Method, e, p);
        //}

        //internal TRet InternalInvokeVarRet<TRet, var0, var1>(var0 e, var1 p)
        //{
        //    return InternalInvokeVarRet<TRet, var0, var1>(Target, Method, e, p);
        //}
        //#endregion

        //#region Invoke
        //protected void Invoke()
        //{
        //    VirtualInvokeImpl();
        //}

        //protected void InvokeVar<T>(T e)
        //{
        //    VirtualInvokeVarImpl(e);
        //}

        //protected void InvokeVar<var0, var1>(var0 e, var1 p)
        //{
        //    VirtualInvokeVarImpl(e, p);
        //}

        //protected TRet InvokeVarRet<TRet, var0, var1>(var0 e, var1 p)
        //{
        //    return VirtualInvokeVarImplRet<TRet, var0, var1>(e, p);
        //}

        //#endregion

        //#region InvokeVarImpl

        //protected virtual TRet VirtualInvokeVarImplRet<TRet, var0, var1>(var0 e, var1 p)
        //{
        //    return InternalInvokeVarRet<TRet, var0, var1>(e, p);
        //}

        //protected virtual void VirtualInvokeVarImpl<var0, var1>(var0 e, var1 p)
        //{
        //    InternalInvokeVar(e, p);
        //}

        //protected virtual void VirtualInvokeVarImpl<T>(T e)
        //{
        //    InternalInvokeVar(e);
        //}

        //protected virtual void VirtualInvokeImpl()
        //{
        //    InternalInvoke();
        //}
        //#endregion

        //#region static InternalInvoke
        //[Script(OptimizedCode = "if ($o) $o->$p(); else $p();")]
        //internal static void InternalInvoke([ScriptParameterByRef] object o, global::System.IntPtr p)
        //{

        //}

        //[Script(OptimizedCode = "if ($o) $o->$p($a); else $p($a);")]
        //internal static void InternalInvokeVar<T>([ScriptParameterByRef] object o, global::System.IntPtr p, T a)
        //{

        //}

        //[Script(OptimizedCode = "if ($o) $o->$p($a0, $a1); else $p($a0, $a1);")]
        //internal static void InternalInvokeVar<var0, var1>([ScriptParameterByRef] object o, global::System.IntPtr p, var0 a0, var1 a1)
        //{

        //}

        //[Script(OptimizedCode = "return $o ? $o->$p($a0, $a1) : $p($a0, $a1);")]
        //internal static TRet InternalInvokeVarRet<TRet, var0, var1>([ScriptParameterByRef] object o, global::System.IntPtr p, var0 a0, var1 a1)
        //{
        //    return default(TRet);
        //}
        //#endregion

        public static DelegateImpl Combine([ScriptParameterByRef] DelegateImpl a,[ScriptParameterByRef] DelegateImpl b)
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

            return default(DelegateImpl);
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
            if (d.Equals(this))
            {
                return null;
            }

            return this;
        }

        public override bool Equals(object obj)
        {
            DelegateImpl v = (DelegateImpl)obj;


            return v.Method == this.Method && v.Target == this.Target;
        }

        public override int GetHashCode()
        {
            return default(int);
        }
    }
}