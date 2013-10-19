using ScriptCoreLib.JavaScript.DOM.HTML;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.DataGridViewCellStyle))]
    internal class __DataGridViewCellStyle : __DataGridViewElement
    {
        public Font Font { get; set; }

        public DataGridViewContentAlignment Alignment { get; set; }

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

        #region BackColor
        public Action InternalBackColorChanged;

        public Color InternalBackColor;
        public Color BackColor
        {
            get
            {
                return InternalBackColor;
            }
            set
            {
                InternalBackColor = value;

                if (InternalBackColorChanged != null)
                    InternalBackColorChanged();
            }


        }
        #endregion


        // needs to be tested
        public DataGridViewTriState WrapMode { get; set; }
        public Color SelectionBackColor { get; set; }
        public Color SelectionForeColor { get; set; }

        public __DataGridViewCellStyle()
        {
            this.InternalForeColor = SystemColors.WindowText;
            this.InternalBackColor = SystemColors.Window;

            this.SelectionBackColor = SystemColors.Highlight;
        }


        #region operators
        public static implicit operator __DataGridViewCellStyle(DataGridViewCellStyle c)
        {
            return (__DataGridViewCellStyle)(object)c;
        }
        public static implicit operator DataGridViewCellStyle(__DataGridViewCellStyle c)
        {
            return (DataGridViewCellStyle)(object)c;
        }
        #endregion
    }
}
