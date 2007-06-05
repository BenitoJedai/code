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
    class DynamicEnumerator<T> : IEnumerator<T>
    {
        public Func<T> DynamicCurrent;

        #region IEnumerator<T> Members

        public T Current
        {
            get { return DynamicCurrent(); }
        }

        #endregion

        public Action DynamicDispose;

        #region IDisposable Members

        public void Dispose()
        {
            DynamicDispose();
        }

        #endregion

        #region IEnumerator Members

        object System.Collections.IEnumerator.Current
        {
            get { return this.Current; }
        }

        public Func<bool> DynamicMoveNext;

        public bool MoveNext()
        {
            return DynamicMoveNext();
        }

        public void Reset()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }

}
