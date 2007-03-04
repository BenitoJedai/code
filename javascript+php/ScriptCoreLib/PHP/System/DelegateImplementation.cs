using ScriptCoreLib.Shared;


namespace ScriptCoreLib.PHP.System
{


    [Script(Implements = typeof(EventHandler))]
    internal class EventHandlerImpl : System.MulticastDelegateImpl
    {
        public EventHandlerImpl(object e, global::System.IntPtr p) : base(e, p) { }
        public new void Invoke() { base.Invoke(); }
    }


    [Script(Implements = typeof(EventHandler<>))]
    internal class EventHandlerImpl<var0> : System.MulticastDelegateImpl
    {
        public EventHandlerImpl(object e, global::System.IntPtr p) : base(e, p) { }
        public void Invoke(var0 e) { base.InvokeVar(e); }
    }

    [Script(Implements = typeof(EventHandler<,>))]
    internal class EventHandlerImpl<var0, var1> : System.MulticastDelegateImpl
    {
        public EventHandlerImpl(object e, global::System.IntPtr p) : base(e, p) { }
        public void Invoke(var0 e, var1 p) { base.InvokeVar(e, p); }
    }

    [Script(Implements = typeof(EventHandler<,,>))]
    internal class EventHandlerImpl<TRet, var0, var1> : System.MulticastDelegateImpl
    {
        public EventHandlerImpl(object e, global::System.IntPtr p) : base(e, p) { }
        public TRet Invoke(var0 e, var1 p) { return base.InvokeVarRet<TRet, var0, var1>(e, p); }
    }
}
