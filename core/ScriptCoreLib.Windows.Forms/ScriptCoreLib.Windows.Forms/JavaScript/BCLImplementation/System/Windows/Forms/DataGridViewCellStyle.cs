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
        public Font Font { get; set; }

        #region ForeColor
        public Action InternalForeColorChanged;

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

                if (InternalForeColorChanged != null)
                    InternalForeColorChanged();
            }


        }
        #endregion

        public __DataGridViewCellStyle()
        {
            this.InternalForeColor = SystemColors.WindowText;
        }
    }
}
