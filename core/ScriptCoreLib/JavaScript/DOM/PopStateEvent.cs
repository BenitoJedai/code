using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/events/PopStateEvent.idl
    [Script(HasNoPrototype = true, ExternalTarget = "PopStateEvent")]
    public class PopStateEvent : IEvent
    {
        // X:\jsc.svn\core\ScriptCoreLib.Async\ScriptCoreLib.Async\JavaScript\DOM\HistoryExtensions.cs

        // 20140809
        // now that we suppot trivial scope sharing for Task.Run
        // we should enable it for historic movements too...
        public readonly object state;
    }


    partial class IWindow
    {

        #region event onpopstate
        [Obsolete(@"handled by X:\jsc.svn\core\ScriptCoreLib.Async\ScriptCoreLib.Async\JavaScript\DOM\HistoryExtensions.cs")]
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
    }
}
