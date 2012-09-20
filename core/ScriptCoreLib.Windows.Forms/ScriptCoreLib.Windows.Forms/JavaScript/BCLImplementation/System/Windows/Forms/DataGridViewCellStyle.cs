using ScriptCoreLib.JavaScript.DOM.HTML;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.DataGridViewCellStyle))]
    internal class __DataGridViewCellStyle : __DataGridViewElement
    {
        public Func<IHTMLElement> GetInternalElement;

        public Color InternalForeColor;
        public Color ForeColor
        {
            get
            {
                return InternalForeColor;
            }
            set
            {
                InternalForeColor = value;

                GetInternalElement().style.color = InternalForeColor.ToString();
            }
        }
    }
}
