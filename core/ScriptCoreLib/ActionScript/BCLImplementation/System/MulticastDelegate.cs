using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.MulticastDelegate))]
    internal class __MulticastDelegate : __Delegate
    {
        [ScriptDelegateDataHint(ScriptDelegateDataHintAttribute.FieldType.List)]
        Array list = new Array();


        public __MulticastDelegate(object e, global::System.IntPtr p)
            : base(e, p)
        {
         
        }
    }
}
