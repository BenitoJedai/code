using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // DOM is not actually the correct namespace, DOM is unavailable within workers isnt it?

    [Script(HasNoPrototype = true)]
    public class SharedWorkerGlobalScope : WorkerGlobalScope
    {
        // tested by?
        // X:\jsc.svn\examples\javascript\Test\TestServiceWorkerRegistrations\TestServiceWorkerRegistrations\Application.cs




        //public readonly ApplicationCache applicationCache;
        public readonly string name;


        // chrome://inspect/#


        #region event onmessage
        public event Action<MessageEvent> onconnect
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "connect");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "connect");
            }
        }
        #endregion

    }
}
