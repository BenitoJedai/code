using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    using ScriptCoreLib.JavaScript.DOM;

    [Script(Implements = typeof(global::System.MulticastDelegate))]
    internal class __MulticastDelegate : __Delegate
    {
        [ScriptDelegateDataHint(ScriptDelegateDataHintAttribute.FieldType.List)]
        IArray<__Delegate> list = new IArray<__Delegate>();

        public __MulticastDelegate(object e, global::System.IntPtr p)
            :
            base(e, p)
        {
            list.push(this);
        }

        protected override __Delegate CombineImpl(__Delegate d)
        {
            list.push(d);

            return this;
        }

        

    }

}
