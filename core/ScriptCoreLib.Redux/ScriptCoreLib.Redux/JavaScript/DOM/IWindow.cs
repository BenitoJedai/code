using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    public class IWindow : IEventTarget
    {



        // http://caniuse.com/#search=hash

        #region event onhashchange
        public event System.Action<HashChangeEvent> onhashchange
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "hashchange");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "hashchange");
            }
        }
        #endregion






        public void postMessage(object message, object[] transfer, string targetOrigin = "*")
        {

            // http://www.whatwg.org/specs/web-apps/current-work/#the-window-object
        }

        public void postMessage(object message, string targetOrigin = "*", params object[] transfer)
        {

            // http://www.whatwg.org/specs/web-apps/current-work/#the-window-object
        }



        // https://developer.mozilla.org/en-US/docs/Web/API/window.performance
        public Performance performance;

        // http://caniuse.com/deviceorientation
        #region event orientationchange
        public event Action<IEvent> onorientationchange
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "orientationchange");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "orientationchange");
            }
        }
        #endregion

        public int orientation;   // updates the angle: 0, 90, 180, or -90



        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201303/20130330-cache-manifest
        [Obsolete]
        public ApplicationCache applicationCache;

        #region event ononline
        public event System.Action<IEvent> ononline
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "online");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "online");
            }
        }
        #endregion

        #region event ononline
        public event System.Action<IEvent> onoffline
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "offline");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "offline");
            }
        }
        #endregion
    }
}
