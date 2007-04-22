using ScriptCoreLib.PHP.Runtime;


namespace ScriptCoreLib.PHP.System
{
    // FIXME: User Defined eventhandlers wont be able to remove delegates

    [Script(Implements = typeof(global::System.MulticastDelegate))]
    internal class MulticastDelegateImpl : DelegateImpl
    {
        [ScriptDelegateDataHint(ScriptDelegateDataHintAttribute.FieldType.List)]
        IArray<DelegateImpl> list = new IArray<DelegateImpl>();

        public MulticastDelegateImpl([ScriptParameterByRef] object e, global::System.IntPtr p)
            :
            base(e, p)
        {
            list.Push(this);
        }

        protected override DelegateImpl CombineImpl(DelegateImpl d)
        {
            list.Push(d);

            return this;
        }

        protected override DelegateImpl RemoveImpl(DelegateImpl d)
        {
            // ???

            return this;
        }
        //protected override void VirtualInvokeVarImpl<T>(T e)
        //{
        //    foreach (DelegateImpl d in list.ToArray())
        //        d.InternalInvokeVar(e);
        //}

        //protected override void VirtualInvokeImpl()
        //{

        //    foreach (DelegateImpl d in list.ToArray())
        //        d.InternalInvoke();

        //}

        //protected override TRet VirtualInvokeVarImplRet<TRet, var0, var1>(var0 e, var1 p)
        //{
        //    TRet r = default(TRet);

        //    foreach (DelegateImpl d in list.ToArray())
        //        r = d.InternalInvokeVarRet<TRet, var0, var1>(e, p);

        //    return r;
            
        //}

    }
}