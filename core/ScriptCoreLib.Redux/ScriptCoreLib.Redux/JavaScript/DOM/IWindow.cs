using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.FileAPI;
using ScriptCoreLib.JavaScript.HistoryAPI;
using ScriptCoreLib.JavaScript.MessagingAPI;

namespace ScriptCoreLib.JavaScript.DOM
{
    public class IWindow : ISink
    {
        public History history;

        #region event
        public event System.Action<PopStateEvent> onpopstate
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "popstate");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "popstate");
            }
        }
        #endregion

        public IWindow parent;

        public void postMessage(object message, string targetOrigin = "*")
        {
            // http://www.whatwg.org/specs/web-apps/current-work/#the-window-object
        }

        #region event
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
    }
}
