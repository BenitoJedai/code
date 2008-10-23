using ScriptCoreLib.PHP.Runtime;


namespace ScriptCoreLib.PHP.BCLImplementation.System
{
    // FIXME: User Defined eventhandlers wont be able to remove delegates

    [Script(Implements = typeof(global::System.MulticastDelegate))]
    internal class __MulticastDelegate : __Delegate
    {
        [ScriptDelegateDataHint(ScriptDelegateDataHintAttribute.FieldType.List)]
        IArray<__Delegate> list = new IArray<__Delegate>();

        public __MulticastDelegate([ScriptParameterByRef] object e, global::System.IntPtr p)
            :
            base(e, p)
        {
            list.Push(this);
        }

        protected override __Delegate CombineImpl(__Delegate d)
        {
            list.Push(d);

            return this;
        }

        protected override __Delegate RemoveImpl(__Delegate d)
        {
            // ???

            return this;
        }
        

    }
}