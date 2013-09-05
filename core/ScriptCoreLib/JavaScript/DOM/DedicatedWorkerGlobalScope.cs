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



        public int setTimeout(IFunction code, int time)
        {
            return default(int);
        }

        [Script(DefineAsStatic = true)]
        internal int setTimeout(System.Action code, int time)
        {
            return setTimeout(((BCLImplementation.System.__Delegate)((object)code)).InvokePointer, time);
        }

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
