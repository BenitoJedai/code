using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.FileAPI;
using ScriptCoreLib.JavaScript.HistoryAPI;

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
    }
}
