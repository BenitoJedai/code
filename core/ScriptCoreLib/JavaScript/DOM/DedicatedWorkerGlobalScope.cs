using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/workers/DedicatedWorkerGlobalScope.idl
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/crypto/WorkerGlobalScopeCrypto.idl




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

        // tested by?
        #region event onfirstmessage
        public event System.Action<MessageEvent> onfirstmessage
        {
            [Script(DefineAsStatic = true)]
            add
            {

                System.Action<MessageEvent> yield = null;

                yield = e =>
                {
                    value(e);


                    this.onmessage -= yield;
                };

                this.onmessage += yield;

            }
            [Script(DefineAsStatic = true)]
            remove
            {
            }
        }
        #endregion

        public void postMessage(object message) { }
    }
}
