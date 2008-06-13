using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.Nonoba.api;

namespace ScriptCoreLib.ActionScript.Extensions.Nonoba.api
{
    [Script(Implements = typeof(Message))]
    internal class __Message
    {
        [Script(OptimizedCode = "that.Clone(to);")]
        internal static void DynamicClone(object that, object to)
        {
        }

        public static Message CloneFrom(object e)
        {
            var Type = (string)DynamicContainer.GetValue(e, "Type");
            
            var n = new Message(Type);

            DynamicClone(e, n);

            return n;
        }
    }
}
