using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.Nonoba.api;

namespace ScriptCoreLib.ActionScript.Extensions.Nonoba.api
{
    [Script(Implements = typeof(MessageEvent))]
    internal class __MessageEvent
    {
        public static MessageEvent CloneFrom(object e)
        {
            var type = (string)DynamicContainer.GetValue(e, "type");
            var message = DynamicContainer.GetValue(e, "message");

            return new MessageEvent(type, Message.CloneFrom(message));
        }
    }
}
