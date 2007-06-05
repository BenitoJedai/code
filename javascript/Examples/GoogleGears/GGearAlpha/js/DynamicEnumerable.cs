//using System.Linq;

using ScriptCoreLib;
using ScriptCoreLib.Shared;

using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Query;
using ScriptCoreLib.Shared.Lambda;

using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM.XML;
using System;

using global::System.Collections.Generic;


namespace GGearAlpha.js
{

    [Script]
    class DynamicEnumerable<T> : IEnumerable<T>
    {
        public Func<IEnumerator<T>> DynamicGetEnumerator;

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return DynamicGetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return DynamicGetEnumerator();
        }

        #endregion
    }

}
