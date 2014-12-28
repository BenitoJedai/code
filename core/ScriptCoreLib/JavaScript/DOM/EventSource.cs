using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://www.chromestatus.com/features/5311740673785856

    [Script(HasNoPrototype = true, ExternalTarget = "EventSource")]
    public class EventSource : IEventTarget
    {
        // tested by?
        // what about service worker?
        // will event source be the push api?
        // would app engine support long running streams?

        // https://code.google.com/p/chromium/issues/detail?id=264170
        public EventSource(string url = "/event-stream")
        {
        }

        #region event onopen
        public event Action<IEvent> onopen
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "open");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "open");
            }
        }
        #endregion

        #region event onerror
        public event Action<IEvent> onerror
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "error");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "error");
            }
        }
        #endregion

        #region event onmessage
        public event Action<MessageEvent> onmessage
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

        public Action<MessageEvent> this[string EventName]
        {
            [Script(DefineAsStatic = true)]
            set
            {
                base.InternalEvent(true, value, EventName);

            }

            //[Script(DefineAsStatic = true)]
            //get
            //{
            //    return null;
            //}
        }
    }
}
