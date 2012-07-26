using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.FileAPI;
using ScriptCoreLib.JavaScript.HistoryAPI;
using ScriptCoreLib.JavaScript.MessagingAPI;
using ScriptCoreLib.JavaScript.StorageAPI;
using ScriptCoreLib.JavaScript.TimingAPI;

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


        // http://www.whatwg.org/specs/web-apps/current-work/multipage/browsers.html#the-window-object
        public IWindow parent;
        public IWindow opener;
        public IWindow top;
        public IWindow self;
        public IWindow window;

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

        public Storage sessionStorage;
        public Storage localStorage;

        public Performance performance;

    }
}
