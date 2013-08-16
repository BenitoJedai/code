using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    [Script(HasNoPrototype = true)]
    public class WorkerGlobalScope : IEventTarget
    {

        public readonly WorkerLocation location;
    
    }

    [Script(HasNoPrototype = true)]
    public class WorkerLocation
    {
        public readonly string href;
    }


    [Script(HasNoPrototype = true)]
    public class DedicatedWorkerGlobalScope : WorkerGlobalScope
    {
        #region event onmessage
        public event System.Action<MessageEvent> onmessage
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "message");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "message");
            }
        }
        #endregion



        public void postMessage(object message) { }
    }
}
