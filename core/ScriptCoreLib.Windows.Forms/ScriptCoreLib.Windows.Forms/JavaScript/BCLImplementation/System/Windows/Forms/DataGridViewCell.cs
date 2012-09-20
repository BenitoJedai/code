using ScriptCoreLib.JavaScript.DOM.HTML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.DataGridViewCell))]
    internal abstract class __DataGridViewCell : __DataGridViewElement
    {
        public IHTMLDiv InternalContentContainer;
        public IHTMLTableColumn InternalTableColumn;

        public object InternalValue;
        public event Action InternalValueChanged;
        public object Value
        {
            get
            {
                return InternalValue;
            }
            set
            {
                InternalValue = value;
                if (InternalValueChanged != null)
                    InternalValueChanged();
            }
        }
    }
}
