using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    [Script(HasNoPrototype = true, ExternalTarget = "DragEvent")]
    public class DragEvent : IEvent
    {
        public readonly DataTransfer dataTransfer;

        public DragEvent(string type) { }
    }
}
