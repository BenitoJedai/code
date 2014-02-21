using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    [Script(HasNoPrototype = true, ExternalTarget = "MessageEvent")]
    public class MessageEvent : IEvent
    {
        // rename to window?
        public IWindow source;


        // X:\jsc.svn\examples\javascript\NewWindowMessagingExperiment\NewWindowMessagingExperiment\Application.cs
        public string origin;

        public object data;

        public readonly MessagePort[] ports;

    }
}
