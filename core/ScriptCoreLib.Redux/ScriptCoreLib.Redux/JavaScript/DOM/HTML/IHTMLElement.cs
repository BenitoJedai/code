using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    public class IHTMLElement : IElement
    {
        #region event ondragover
        public event System.Action<DragEvent> ondragover
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "dragover");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "dragover");
            }
        }
        #endregion

        #region event ondragleave
        public event System.Action<DragEvent> ondragleave
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "dragleave");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "dragleave");
            }
        }
        #endregion

        #region event ondrop
        public event System.Action<DragEvent> ondrop
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "drop");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "drop");
            }
        }
        #endregion
    }
}
