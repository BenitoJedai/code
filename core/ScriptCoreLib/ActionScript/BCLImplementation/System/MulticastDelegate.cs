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
        protected Array list = new Array();


        public __MulticastDelegate(object e, global::System.IntPtr p)
            : base(e, p)
        {
            list.push(this);
        }



        protected override __Delegate CombineImpl(__Delegate d)
        {
            list.push(d);

            return this;
        }

        protected override __Delegate RemoveImpl(__Delegate d)
        {
            var j = -1;
            var a = ((__Delegate[])(object)list);

            
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == d)
                {
                    j = i;
                    break;
                }
            }

            if (j > -1)
                list.splice(j, 1);

            if (list.length == 0)
                return null;

            return this;
        }
    }
}
