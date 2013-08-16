using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    [Script(HasNoPrototype = true, ExternalTarget = "MessageEvent")]
    public class MessageEvent : IEvent
    {
        public object data;

        public readonly MessagePort[] ports;

    }
}
